using Horizon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Domain.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAll();
    Task<Role> GetById(Guid id);
    Task Create(Role role);
    Task Update(Role role);
    Task Delete(Guid id);
}
