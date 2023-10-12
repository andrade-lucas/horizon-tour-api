using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Inputs;
using Horizon.Domain.Queries.Responses.Places;
using Horizon.Shared.Outputs;

namespace Horizon.Domain.Repositories;

public interface IPlaceRepository
{
    Task<PaginationResult<GetPlacesResponse>> GetByUserAsync(string userId, QueryPaginate queryPaginate);

    Task<Place> GetByIdAsync(string id);

    Task CreateAsync(Place place);

    Task PublishAsync(Place placeId);
}
