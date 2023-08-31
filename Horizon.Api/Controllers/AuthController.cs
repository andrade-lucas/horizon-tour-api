using Horizon.Auth.Command.Inputs;
using Horizon.Auth.Repositories;
using Horizon.Auth.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Horizon.Auth.Command.Handlers;
using Horizon.Domain.Repositories;
using Horizon.Shared.Commands;

namespace Horizon.Api.Controllers;

[Route("v1/auth")]
public class AuthController : ControllerBase
{
    private readonly ICommandHandler<LoginCommand> _loginHandler;
    private readonly ICommandHandler<RegisterUserCommand> _registerUserHandler;

    public AuthController(ICommandHandler<LoginCommand> loginHandler, ICommandHandler<RegisterUserCommand> registerUserHandler)
    {
        _loginHandler = loginHandler;
        _registerUserHandler = registerUserHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _loginHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterUserCommand command)
    {
        var result = await _registerUserHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }
}
