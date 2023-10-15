using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Commands.Inputs.Users;

public record DeleteUserCommand(string UserId) : IRequest<IResult>;
