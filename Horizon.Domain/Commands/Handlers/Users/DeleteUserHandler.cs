using Horizon.Domain.Commands.Inputs.Users;
using Horizon.Shared.Messages;
using Horizon.Domain.Repositories;
using Horizon.Shared.Contracts;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Users;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, IResult>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (command.UserId == null || command.UserId == string.Empty)
                return new CommandResult(false, Messages.Error, (int)HttpStatusCode.BadRequest);

            await _userRepository.DeleteAsync(command.UserId);

            return new CommandResult(true, string.Format(Messages.DeletedSuccess, Fields.User), (int)HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new CommandResult(false, Messages.Error, (int)HttpStatusCode.InternalServerError);
        }
    }
}
