using Horizon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();

    Task<User> GetById(Guid id);

    Task Delete(Guid id);
}
