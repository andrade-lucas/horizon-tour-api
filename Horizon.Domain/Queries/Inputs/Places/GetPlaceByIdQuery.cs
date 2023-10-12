using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Queries.Inputs.Places;

public record GetPlaceByIdQuery(string PlaceId) : IRequest<IResult>;
