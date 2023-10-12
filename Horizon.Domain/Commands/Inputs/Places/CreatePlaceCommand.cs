using Horizon.Domain.Enums;
using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Commands.Inputs.Places;

public record CreatePlaceCommand(
    string Name,
    string OwnerId,
    string CityId,
    string Street,
    string Number,
    string ZipCode,
    string Neighborhood,
    double Latitude,
    double Longitude,
    EAutomaticOpen AutomaticOpen,
    EPlaceType Type,
    string Description,
    string PresentationImageBase64
) : IRequest<IResult>;
