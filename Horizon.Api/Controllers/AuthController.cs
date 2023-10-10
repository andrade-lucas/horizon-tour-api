using Horizon.Auth.Command.Inputs;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Horizon.Api.Controllers;

[Route("v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }
}
