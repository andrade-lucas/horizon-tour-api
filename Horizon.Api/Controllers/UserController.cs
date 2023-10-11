using Horizon.Api.Controllers.Requests;
using Horizon.Domain.Commands.Inputs.Users;
using Horizon.Domain.Queries.Inputs.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Route("v1/users")]
[Authorize(Roles = "admin")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginateRequest request)
    {
        var query = new GetAllUsersQuery(request.Filter, request.Page, request.PageSize);
        var result = await _mediator.Send(query);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }
}
