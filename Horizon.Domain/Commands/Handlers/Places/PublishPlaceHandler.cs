using Horizon.Domain.Commands.Inputs.Places;
using Horizon.Domain.Repositories;
using Horizon.Shared.Contracts;
using Horizon.Shared.Messages;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Places;

public class PublishPlaceHandler : IRequestHandler<PublishPlaceCommand, IResult>
{
    private readonly IPlaceRepository _placeRepository;

    public PublishPlaceHandler(IPlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
    }

    public async Task<IResult> Handle(PublishPlaceCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var place = await _placeRepository.GetByIdAsync(command.PlaceId);

            if (place == null) 
                return new CommandResult(false, string.Format(Messages.NotFound, "Local"), (int)HttpStatusCode.NotFound);
            
            var canPublish = place.CanPublish();
            if (!canPublish)
            {
                var errors = place.ErrorsOnPublish();
                return new CommandResult(
                    false, 
                    string.Format(Messages.CannotPublish), 
                    (int)HttpStatusCode.NotFound,
                    errors: errors
                );
            }

            place.Publish();
            await _placeRepository.PublishAsync(place);

            return new CommandResult(true, string.Format(Messages.PublishedSuccess, "Local"), (int)HttpStatusCode.OK);
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
