using Horizon.Auth.Command.Inputs;
using Horizon.Auth.Repositories;
using Horizon.Auth.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Horizon.Auth.Command.Handlers;

namespace Horizon.Api.Controllers;

[Route("v1/auth")]
public class AuthController : ControllerBase
{
    private readonly LoginHandler _loginHandler;
    private readonly RegisterUserHandler _registerUserHandler;

    public AuthController(IAuthRepository authRepository, ITokenService tokenService)
    {
        _loginHandler = new LoginHandler(authRepository, tokenService);
        _registerUserHandler = new RegisterUserHandler(authRepository);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _loginHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterUserCommand command)
    {
        var result = await _registerUserHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }
}
