using Horizon.Domain.Commands.Inputs.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Horizon.Domain.Queries.Inputs.Roles;
using MediatR;
using Horizon.Api.Controllers.Requests.Roles;

namespace Horizon.Api.Controllers;

[Route("v1/roles")]
[Authorize(Roles = "admin")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var query = new GetRolesQuery();
        var result = await _mediator.Send(query);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request)
    {
        var command = new CreateRoleCommand(request.Name, request.Slug);
        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }
}
