using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Queries.Inputs.Users;

public record GetAllUsersQuery(
    string? Filter = null,
    int Page = 0,
    int PageSize = 20
) : IRequest<IResult>;
