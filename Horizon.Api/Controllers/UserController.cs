using Horizon.Domain.Commands.Inputs.Users;
using Horizon.Domain.Queries.Inputs.Users;
using Horizon.Shared.Commands;
using Horizon.Shared.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Route("v1/users")]
[Authorize(Roles = "admin")]
public class UserController : ControllerBase
{
    private readonly ICommandHandler<DeleteUserCommand> _deleteUserHandler;
    private readonly IQueryHandler<GetAllUsersQuery> _getAllUsersQuery;

    public UserController(ICommandHandler<DeleteUserCommand> deleteUserHandler, IQueryHandler<GetAllUsersQuery> getAllUsersQuery)
    {
        _deleteUserHandler = deleteUserHandler;
        _getAllUsersQuery = getAllUsersQuery;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query)
    {
        var result = await _getAllUsersQuery.Handle(query);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteUserCommand { UserId = id };
        var result = await _deleteUserHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }
}
