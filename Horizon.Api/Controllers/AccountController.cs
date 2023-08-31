using Horizon.Domain.Queries.Inputs.Account;
using Horizon.Shared.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Horizon.Api.Controllers;

[Authorize]
[Route("v1/account")]
public class AccountController : ControllerBase
{
    private readonly ICommandHandler<GetCurrentUserCommand> _currentUserHandler;

    public AccountController(ICommandHandler<GetCurrentUserCommand> currentUserHandler)
    {
        _currentUserHandler = currentUserHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentAsync()
    {
        var command = new GetCurrentUserCommand
        {
            UserId = User.FindFirst("id")?.Value
        };

        var result = await _currentUserHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }
}
