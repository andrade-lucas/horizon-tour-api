using Dapper;
using Horizon.Auth.Repositories;
using Horizon.Domain.Entities;
using Horizon.Auth.Queries.Responses;
using Horizon.Domain.Repositories;
using Horizon.Domain.ValueObjects;
using Horizon.Infra.Context;
using System.Data;
using System.Diagnostics;

namespace Horizon.Infra.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly IDB _db;
    private readonly IUsersRolesRepository _usersRolesRepository;

    public AuthRepository(IDB db, IUsersRolesRepository usersRolesRepository)
    {
        _db = db;
        _usersRolesRepository = usersRolesRepository;
    }

    public async Task<bool> EmailExistsAsync(Email email)
    {
        var sql = "SELECT COUNT(*) FROM users WHERE Email = @email";

        return await _db.Connection().ExecuteScalarAsync<bool>(sql, new 
        { 
            email = email.ToString() 
        });
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var query = await _db.Connection().QueryAsync<GetUserByEmailResponse>(
            "SELECT Id, FirstName, LastName, NickName, Email, Password, ProfileImageUrl, Verified FROM users WHERE email = @email",
            new
            {
                email = email
            }
        );

        var result = query.First();

        return new User(
            result.Id,
            new Name(result.FirstName, result.LastName, result.NickName),
            new Email(result.Email),
            new Password(result.Password),
            profileImageUrl: result.ProfileImageUrl,
            verified: result.Verified
        );
    }

    public async Task CreateAsync(User user)
    {
        var conn = _db.Connection();
        conn.Open();
        var trans = conn.BeginTransaction();

        try
        {
            var result = await _db.Connection().ExecuteAsync(
                "INSERT INTO users " +
                "(Id, FirstName, LastName, NickName, Email, Password, CreatedAt, UpdatedAt) " +
                "VALUES(@id, @firstName, @lastName, @nickName, @email, @password, @createdAt, @updatedAt);",
                new
                {
                    id = user.Id,
                    firstName = user.Name.FirstName,
                    lastName = user.Name.LastName,
                    nickName = user.Name.NickName,
                    email = user.Email.Address,
                    password = user.Password.Value,
                    createdAt = user.CreatedAt,
                    updatedAt = user.UpdatedAt
                },
                transaction: trans
            );

            foreach (var role in user.Roles)
            {
                await _usersRolesRepository.AddUserToRoleAsync(user, role, trans);
            }

            trans.Commit();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw ex;
        }
    }
}
