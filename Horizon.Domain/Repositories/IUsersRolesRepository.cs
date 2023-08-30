using Horizon.Domain.Entities;
using System.Data;

namespace Horizon.Domain.Repositories;

public interface IUsersRolesRepository
{
    Task AddUserToRoleAsync(User user, Role role, IDbTransaction? transaction = null);

    Task RemoveUserFromRole(User user, Role role, IDbTransaction? transaction = null);
}
