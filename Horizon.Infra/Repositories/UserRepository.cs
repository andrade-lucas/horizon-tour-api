using Dapper;
using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Responses.Account;
using Horizon.Domain.Repositories;
using Horizon.Infra.Context;
using Horizon.Domain.Extensions;
using Horizon.Domain.Queries.Responses.Users;
using Horizon.Shared.Helpers;
using Horizon.Shared.Outputs;

namespace Horizon.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDB _db;

    public UserRepository(IDB db)
    {
        _db = db;
    }

    public async Task<PaginationResult<GetAllUsersResponse>> GetAllAsync(string? filter, int page = 0, int pageSize = 20)
    {
        try
        {
            var offset = PaginationHelper.GetOffset(page, pageSize);
            var filterSql = filter != null 
                ? "AND (FirstName LIKE @filter OR LastName LIKE @filter OR NickName LIKE @filter OR Email LIKE @filter)" 
                : "";

            var sql = $@"
                SELECT COUNT(*) AS TOTAL_ROWS FROM users WHERE DeletedAt IS NULL {filterSql}; 
                
                SELECT Id, CONCAT(FirstName, ' ', LastName) AS FullName, NickName, Email, Verified, ProfileImageUrl 
                FROM users
                WHERE DeletedAt IS NULL
                {filterSql}
                LIMIT @offset, @pageSize;
            ";

            var multi = await _db.Connection().QueryMultipleAsync(sql, new { filter = $"%{filter}%", offset, pageSize });
            var totalRows = multi.Read<int>().Single();
            var rows = multi.Read<GetAllUsersResponse>().ToList();

            return new PaginationResult<GetAllUsersResponse>(totalRows, rows.Count, rows);
        }
        catch
        {
            throw;
        }
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

    public async Task DeleteAsync(string id)
    {
        try
        {
            var sql = "UPDATE users SET DeletedAt = @deletedAt WHERE Id = @id";

            await _db.Connection().ExecuteAsync(sql, new
            {
                id = id,
                deletedAt = DateTime.Now.ToFormatedString()
            });
        }
        catch
        {
            throw;
        }
    }
}
