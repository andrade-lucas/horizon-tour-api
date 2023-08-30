using Dapper;
using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Responses.Auth;
using Horizon.Domain.Repositories;
using Horizon.Infra.Context;

namespace Horizon.Infra.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IDB _db;

    public RoleRepository(IDB db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Role>?> GetByUser(string userId)
    {
        var sql = "SELECT r.Id, r.Name, r.Slug FROM roles r " +
            "INNER JOIN users_roles ur ON ur.RoleId = r.Id " +
            "INNER JOIN users u ON u.Id = ur.UserId " +
            "WHERE u.Id = @userId;";

        var result = await _db.Connection().QueryAsync<GetRolesByUserResponse>(sql, new { userId });

        var roles = new List<Role>();
        foreach (var role in result)
            roles.Add(new Role(role.Id, role.Name, role.slug));

        return roles;
    }
}
