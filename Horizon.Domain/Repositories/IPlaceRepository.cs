using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Inputs;
using Horizon.Domain.Queries.Responses.Places;
using Horizon.Shared.Outputs;

namespace Horizon.Domain.Repositories;

public interface IPlaceRepository
{
    Task<PaginationResult<GetPlacesResponse>> GetByOwner(string OwnerId, QueryPaginate queryPaginate);

    Task CreateAsync(Place place);
}
