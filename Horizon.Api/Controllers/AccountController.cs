using Horizon.Domain.Commands.Inputs.Account;
using Horizon.Domain.Queries.Inputs.Account;
using Horizon.Shared.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Authorize]
[Route("v1/account")]
public class AccountController : ControllerBase
{
    private readonly ICommandHandler<GetCurrentUserCommand> _currentUserHandler;
    private readonly ICommandHandler<UpdateAccountCommand> _updateAccountHandler;
    private readonly ICommandHandler<ChangeProfilePictureCommand> _changeProfileImageHandler;

    public AccountController(
        ICommandHandler<GetCurrentUserCommand> currentUserHandler,
        ICommandHandler<UpdateAccountCommand> updateAccountHandler,
        ICommandHandler<ChangeProfilePictureCommand> changeProfileImageHandler
    )
    {
        _currentUserHandler = currentUserHandler;
        _updateAccountHandler = updateAccountHandler;
        _changeProfileImageHandler = changeProfileImageHandler;
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

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateAccountCommand command)
    {
        command.Id = User.FindFirst("id")?.Value;

        var result = await _updateAccountHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPatch("change-profile-picture")]
    public async Task<IActionResult> ChangeProfilePicture([FromBody] ChangeProfilePictureCommand command)
    {
        command.UserId = User.FindFirst("id")?.Value;

        var result = await _changeProfileImageHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }
}
