using Horizon.Domain.Entities;

namespace Horizon.Domain.Repositories
{
    public interface IPlaceRepository
    {
        Task CreateAsync(Place place);
    }
}
