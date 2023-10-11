using Horizon.Domain.Enums;

namespace Horizon.Domain.Queries.Responses.Places;

public record GetPlacesResponse(
    string Id,
    string Name,
    EPlaceStatus Status,
    bool IsOpen,
    string PresentationImageUrl
);
