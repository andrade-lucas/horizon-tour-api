using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Queries.Inputs.Account;

public record GetCurrentUserCommand(
    string UserId
) : IRequest<IResult>;
