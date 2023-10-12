using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Commands.Inputs.Places;

public record PublishPlaceCommand(string PlaceId) : IRequest<IResult>;
