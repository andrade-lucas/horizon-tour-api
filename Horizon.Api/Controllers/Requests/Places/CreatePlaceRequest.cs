using Horizon.Domain.Enums;

namespace Horizon.Api.Controllers.Requests.Places;

public record CreatePlaceRequest(
    string Name,
    EPlaceStatus status,
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
);
