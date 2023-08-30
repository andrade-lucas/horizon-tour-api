using Dapper;
using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Responses.Auth;
using Horizon.Domain.Queries.Responses.Roles;
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

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        var sql = "SELECT * FROM roles";

        var result = await _db.Connection().QueryAsync<GetRolesResponse>(sql);

        var roles = new List<Role>();
        foreach (var role in result)
        {
            roles.Add(new Role(
                role.Id,
                role.Name,
                role.Slug,
                role.CreatedAt,
                role.UpdatedAt)
            );
        }

        return roles;
    }

    public async Task<IEnumerable<Role>?> GetByUserAsync(string userId)
    {
        var sql = "SELECT r.Id, r.Name, r.Slug FROM roles r " +
            "INNER JOIN users_roles ur ON ur.RoleId = r.Id " +
            "INNER JOIN users u ON u.Id = ur.UserId " +
            "WHERE u.Id = @userId;";

        var result = await _db.Connection().QueryAsync<GetRolesResponse>(sql, new { userId });

        var roles = new List<Role>();
        foreach (var role in result)
            roles.Add(new Role(role.Id, role.Name, role.Slug));

        return roles;
    }

    public async Task CreateAsync(Role role)
    {
        var sql = "INSERT INTO roles(Id, Name, Slug, CreatedAt, UpdatedAt) " +
            "VALUES(@id, @name, @slug, @createdAt, @updatedAt)";

        await _db.Connection().ExecuteAsync(sql, new
        {
            id = role.Id,
            name = role.Name,
            slug = role.Slug,
            createdAt = role.CreatedAt,
            updatedAt = role.UpdatedAt
        });
    }

    public async Task<IEnumerable<Role>?> GetDefaultAsync()
    {
        var sql = "SELECT Id, Name, Slug, IsDefault FROM roles WHERE IsDefault = 1";

        var result = await _db.Connection().QueryAsync<GetDefaultRolesResponse>(sql);

        var roles = new List<Role>();
        foreach (var role in result)
        {
            roles.Add(new Role(
                role.Id,
                role.Name,
                role.Slug,
                role.IsDefault)
            );
        }

        return roles;
    }
}
