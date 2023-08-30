using Horizon.Auth.Command.Inputs;
using Horizon.Auth.Repositories;
using Horizon.Domain.Entities;
using Horizon.Domain.Security;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Auth.Command.Handlers;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IAuthRepository _authRepository;

    public RegisterUserHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<ICommandResult> Handle(RegisterUserCommand command)
    {
        var passHash = PasswordHasherSecurity.Hash(command.Password);

        var user = new User(
            new Name(command.FirstName, command.LastName, command.NickName),
            new Email(command.Email),
            new Password(passHash),
            command.Image
        );

        try
        {
            await _authRepository.CreateAsync(user);

            return new CommandResult(true, "User created with success", (int)HttpStatusCode.Created);
        }
        catch (Exception e)
        {
            return new CommandResult(false, e.Message, (int)HttpStatusCode.InternalServerError);
        }
    }
}
