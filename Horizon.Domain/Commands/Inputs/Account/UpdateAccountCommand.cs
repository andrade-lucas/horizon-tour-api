using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Commands.Inputs.Account;

public record UpdateAccountCommand(
    string Id,
    string FirstName,
    string LastName,
    string NickName,
    string Phone,
    DateTime Birthdate
) : IRequest<IResult>;
