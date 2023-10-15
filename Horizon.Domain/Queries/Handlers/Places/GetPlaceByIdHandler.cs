using Horizon.Domain.Queries.Inputs.Places;
using Horizon.Domain.Repositories;
using Horizon.Shared.Contracts;
using Horizon.Shared.Messages;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Queries.Handlers.Places;

public class GetPlaceByIdHandler : IRequestHandler<GetPlaceByIdQuery, IResult>
{
    private readonly IPlaceRepository _placeRepository;

    public GetPlaceByIdHandler(IPlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
    }

    public async Task<IResult> Handle(GetPlaceByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var place = await _placeRepository.GetByIdAsync(query.PlaceId);

            if (place == null)
                return new CommandResult(
                    false,
                    string.Format(Messages.NotFound, "Local"),
                    (int)HttpStatusCode.NotFound
                );

            return new CommandResult(true, string.Empty, (int)HttpStatusCode.OK, place);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new CommandResult(
                false,
                string.Format(Messages.Error),
                (int)HttpStatusCode.InternalServerError
            );
        }
    }
}
