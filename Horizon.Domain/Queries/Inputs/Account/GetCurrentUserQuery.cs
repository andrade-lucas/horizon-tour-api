using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Queries.Inputs.Account;

public record GetCurrentUserQuery(
    string UserId
) : IRequest<IResult>;
