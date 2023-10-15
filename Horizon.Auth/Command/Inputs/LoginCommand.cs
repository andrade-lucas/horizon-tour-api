using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Auth.Command.Inputs;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<IResult>;
