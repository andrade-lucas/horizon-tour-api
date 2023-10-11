using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Queries.Inputs.Roles;

public record GetRolesQuery() : IRequest<IResult>;
