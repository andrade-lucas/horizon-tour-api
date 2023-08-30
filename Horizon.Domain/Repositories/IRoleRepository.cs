using Horizon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Domain.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Role>?> GetByUser(string userId);
}
