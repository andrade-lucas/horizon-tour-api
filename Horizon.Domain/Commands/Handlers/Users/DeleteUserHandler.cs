using Horizon.Domain.Commands.Inputs.Users;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.Repositories;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Users;

public class DeleteUserHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ICommandResult> Handle(DeleteUserCommand command)
    {
        try
        {
            if (command.UserId == null || command.UserId == string.Empty)
                return new CommandResult(false, PtBrMessages.Error, (int)HttpStatusCode.BadRequest);

            await _userRepository.DeleteAsync(command.UserId);

            return new CommandResult(true, string.Format(PtBrMessages.DeletedSuccess, PtBrFields.User), (int)HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new CommandResult(false, PtBrMessages.Error, (int)HttpStatusCode.InternalServerError);
        }
    }
}
