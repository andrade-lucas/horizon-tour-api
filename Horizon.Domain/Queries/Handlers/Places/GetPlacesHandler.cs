using Horizon.Domain.Queries.Inputs;
using Horizon.Domain.Queries.Inputs.Places;
using Horizon.Domain.Repositories;
using Horizon.Shared.Contracts;
using Horizon.Shared.Messages;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Queries.Handlers.Places;

public class GetPlacesHandler : IRequestHandler<GetPlacesQuery, IResult>
{
    private readonly IPlaceRepository _placeRepository;

    public GetPlacesHandler(IPlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
    }

    public async Task<IResult> Handle(GetPlacesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var queryPaginate = new QueryPaginate(query.Filter, query.Page, query.PageSize);

            var places = await _placeRepository.GetByUserAsync(query.UserId, queryPaginate);

            return new CommandResult(true, string.Empty, (int)HttpStatusCode.OK, data: places);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new CommandResult(false, string.Format(Messages.Error), (int)HttpStatusCode.InternalServerError);
        }
    }
}
