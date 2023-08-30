using Dapper;
using Horizon.Auth.Repositories;
using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Responses.Auth;
using Horizon.Domain.ValueObjects;
using Horizon.Infra.Context;
using System.Data;

namespace Horizon.Infra.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly IDB _db;

    public AuthRepository(IDB db)
    {
        _db = db;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var query = await _db.Connection().QueryAsync<GetUserByEmailResponse>(
            "SELECT Id, FirstName, LastName, NickName, Email, Password, ImageUrl FROM users WHERE email = @email",
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
            result.ImageUrl
        );
    }

    public async Task CreateAsync(User user)
    {
        var result = await _db.Connection().QueryAsync(
            "INSERT INTO users " +
            "(Id, FirstName, LastName, NickName, Email, Password, ImageUrl, CreatedAt, UpdatedAt) " +
            "VALUES(@id, @firstName, @lastName, @nickName, @email, @password, @imageUrl, @createdAt, @updatedAt);",
            new
            {
                id = user.Id,
                firstName = user.Name.FirstName,
                lastName = user.Name.LastName,
                nickName = user.Name.NickName,
                email = user.Email.Address,
                password = user.Password.Value,
                imageUrl = user.ImageUrl,
                createdAt = user.CreatedAt,
                updatedAt = user.UpdatedAt
            }
        );
    }
}
