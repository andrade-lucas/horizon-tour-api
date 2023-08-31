using Horizon.Domain.Queries.Inputs.Account;
using Horizon.Domain.Repositories;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Domain.Queries.Handlers.Account;

public class GetCurrentUserHandler : ICommandHandler<GetCurrentUserCommand>
{
    private readonly IUserRepository _userRepository;

    public GetCurrentUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ICommandResult> Handle(GetCurrentUserCommand command)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);

            if (user == null) return new CommandResult(false, "Error on get user", (int)HttpStatusCode.BadRequest);

            return new CommandResult(true, string.Empty, (int)HttpStatusCode.OK, data: user);
        }
        catch(Exception ex)
        {
            return new CommandResult
            {
                Success = false,
                Message = "Internal server error",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Errors = ex.Message
            };
        }
    }
}
