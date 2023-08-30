using Horizon.Auth.Command.Inputs;
using Horizon.Auth.Repositories;
using Horizon.Auth.Services.Contracts;
using Horizon.Domain.Entities;
using Horizon.Domain.Repositories;
using Horizon.Domain.Security;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Auth.Command.Handlers;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IAuthRepository _authRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenService _tokenService;

    public RegisterUserHandler(IAuthRepository authRepository, IRoleRepository roleRepository, ITokenService tokenService)
    {
        _authRepository = authRepository;
        _roleRepository = roleRepository;
        _tokenService = tokenService;
    }

    public async Task<ICommandResult> Handle(RegisterUserCommand command)
    {
        var passHash = PasswordHasherSecurity.Hash(command.Password);

        var name = new Name(command.FirstName, command.LastName, command.NickName);
        var email = new Email(command.Email);
        var password = new Password(passHash);

        var emailExists = await _authRepository.EmailExistsAsync(email);
        if (emailExists)
            return new CommandResult(false, "There is already an user with the provided email", (int)HttpStatusCode.BadRequest);

        var defaultRoles = await _roleRepository.GetDefaultAsync();

        var user = new User(name, email, password);

        if (defaultRoles != null && defaultRoles.Count() > 0)
            user.AddRoleRange(defaultRoles);

        try
        {
            await _authRepository.CreateAsync(user);
            var token = _tokenService.GenerateToken(user);

            return new CommandResult(
                true, 
                "Account was created with success. Welcome ;D", 
                (int)HttpStatusCode.Created,
                token
            );
        }
        catch (Exception e)
        {
            return new CommandResult(false, "Internal server error", (int)HttpStatusCode.InternalServerError, errors: e.Message);
        }
    }
}
