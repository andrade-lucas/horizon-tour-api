using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Domain.Commands.Inputs.Account;

public record ChangeProfilePictureCommand(
    string UserId,
    string ImageBase64
) : IRequest<IResult>;
