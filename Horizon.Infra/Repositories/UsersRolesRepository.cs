using Dapper;
using Horizon.Domain.Entities;
using Horizon.Domain.Repositories;
using Horizon.Infra.Context;
using System.Data;

namespace Horizon.Infra.Repositories;

public class UsersRolesRepository : IUsersRolesRepository
{
    private readonly IDB _db;

    public UsersRolesRepository(IDB db)
    {
        _db = db;
    }

    public async Task AddUserToRoleAsync(User user, Role role, IDbTransaction? transaction = null)
    {
        var sql = "INSERT INTO users_roles(Id, UserId, RoleId, CreatedAt, UpdatedAt) " +
            "VALUES(@id, @userId, @roleId, @createdAt, @updatedAt);";

        await _db.Connection().ExecuteAsync(sql, new
            {
                id = Guid.NewGuid(),
                userId = user.Id.ToString(),
                roleId = role.Id.ToString(),
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now
            },
            transaction: transaction ?? null
        );
    }

    public async Task RemoveUserFromRole(User user, Role role, IDbTransaction? transaction = null)
    {
        var sql = "UPDATE users_roles SET DeletedAt = @deletedAt " +
            "WHERE UserId = @userId AND RoleId = @roleId";

        await _db.Connection().ExecuteAsync(sql, new
        {
            deletedAt = DateTime.Now,
            userId = user.Id.ToString(),
            roleId = role.Id.ToString()
        });
    }
}
