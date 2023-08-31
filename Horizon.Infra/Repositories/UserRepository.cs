using Dapper;
using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Responses.Account;
using Horizon.Domain.Repositories;
using Horizon.Domain.ValueObjects;
using Horizon.Infra.Context;

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
}
