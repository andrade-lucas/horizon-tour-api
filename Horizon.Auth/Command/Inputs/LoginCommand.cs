using Horizon.Shared.Commands;
using MediatR;

namespace Horizon.Auth.Command.Inputs;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<ICommandResult>;
