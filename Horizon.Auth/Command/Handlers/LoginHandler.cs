using Horizon.Auth.Command.Inputs;
using Horizon.Auth.Repositories;
using Horizon.Auth.Services.Contracts;
using Horizon.Domain.Security;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Auth.Command.Handlers;

public class LoginHandler : ICommandHandler<LoginCommand>
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokenService _tokenService;

    public LoginHandler(IAuthRepository authRepository, ITokenService tokenService)
    {
        _authRepository = authRepository;
        _tokenService = tokenService;
    }

    public async Task<ICommandResult> Handle(LoginCommand command)
    {
        var email = new Email(command.Email);
        var password = new Password(command.Password);

        var user = await _authRepository.GetByEmailAsync(email.Address);

        if (user == null)
            return new CommandResult(false, "Email or password is incorrect", (int)HttpStatusCode.BadRequest);

        var verifiedPass = PasswordHasherSecurity.Verify(password.Value, user.Password.Value);

        if (!verifiedPass)
            return new CommandResult(false, "Email or password is incorrect", (int)HttpStatusCode.BadRequest);

        var token = _tokenService.GenerateToken(user);

        return new CommandResult(true, "Welcome", (int)HttpStatusCode.OK, new { token });
    }
}
