using Horizon.Domain.Commands.Inputs.Users;
using Horizon.Shared.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Route("v1/users")]
[Authorize(Roles = "admin")]
public class UserController : ControllerBase
{
    private readonly ICommandHandler<DeleteUserCommand> _deleteUserHandler;

    public UserController(ICommandHandler<DeleteUserCommand> deleteUserHandler)
    {
        _deleteUserHandler = deleteUserHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return StatusCode(200);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteUserCommand { UserId = id };
        var result = await _deleteUserHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }
}
