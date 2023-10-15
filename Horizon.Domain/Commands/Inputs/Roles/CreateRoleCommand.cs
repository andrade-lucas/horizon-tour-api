using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Commands.Inputs.Roles;

public record CreateRoleCommand(
    string Name,
    string Slug
) : IRequest<IResult>;
