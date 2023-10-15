using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Queries.Inputs.Places;

public record GetPlacesQuery(
    string UserId,
    string? Filter = null,
    int Page = 0,
    int PageSize = 20
) : IRequest<IResult>;
