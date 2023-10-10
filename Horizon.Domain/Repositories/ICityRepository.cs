using Horizon.Domain.Entities;

namespace Horizon.Domain.Repositories;

public interface ICityRepository
{
    Task<City> GetByIdAsync(string id);
}
