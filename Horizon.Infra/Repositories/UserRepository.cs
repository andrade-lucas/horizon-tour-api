using Dapper;
using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Responses.Account;
using Horizon.Domain.Repositories;
using Horizon.Infra.Context;
using Horizon.Domain.Extensions;

namespace Horizon.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDB _db;

    public UserRepository(IDB db)
    {
        _db = db;
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<GetCurrentUserResponse> GetByIdAsync(string id)
    {
        var sql = "SELECT Id, FirstName, LastName, NickName, Email, Phone, ProfileImageUrl, Verified, Birthdate " +
            "FROM users " +
            "WHERE Id = @id";

        return await _db.Connection()
            .QueryFirstAsync<GetCurrentUserResponse>(sql, new { id });
    }

    public async Task UploadProfileImageAsync(string userId, string profileImageUrl)
    {
        var sql = "UPDATE users SET ProfileImageUrl = @profileImageUrl " +
            "WHERE Id = @id";

        await _db.Connection().ExecuteAsync(sql, new
        {
            id = userId,
            profileImageUrl
        });
    }

    public async Task<string?> GetCurrentUserProfileUrl(string userId)
    {
        try
        {
            var sql = "SELECT ProfileImageUrl FROM users WHERE Id = @id";

            return await _db.Connection().QueryFirstAsync<string>(sql, new
            {
                id = userId
            });
        }
        catch
        {
            return null;
        }
    }

    public async Task UpdateUserAsync(User user)
    {
        try
        {
            var sql = "UPDATE users SET FirstName = @firstName, LastName = @lastName, NickName = @nickName, Phone = @phone, " +
                "Birthdate = @birthdate, UpdatedAt = @updatedAt WHERE Id = @id";

            await _db.Connection().ExecuteAsync(sql, new
            {
                id = user.Id.ToString(),
                firstName = user.Name.FirstName,
                lastName = user.Name.LastName,
                nickName = user.Name.NickName,
                phone = user.Phone?.Number,
                birthdate = user?.Birthdate.ToFormatedString(),
                updatedAt = user?.UpdatedAt.ToFormatedString()
            });
        }
        catch
        {
            throw;
        }
    }
}
