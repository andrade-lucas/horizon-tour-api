using Horizon.Domain.Commands.Inputs.Roles;
using Horizon.Domain.Commands.Handlers.Roles;
using Horizon.Domain.Repositories;
using Horizon.Shared.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Horizon.Domain.Queries.Inputs.Roles;
using Horizon.Domain.Queries.Handlers.Roles;

namespace Horizon.Api.Controllers;

[Route("v1/roles")]
[Authorize(Roles = "admin")]
public class RolesController : ControllerBase
{
    private readonly ICommandHandler<CreateRoleCommand> _createRoleHandler;
    private readonly ICommandHandler<GetAllCommand> _getAllHandler;

    public RolesController(IRoleRepository roleRepository)
    {
        _createRoleHandler = new CreateRoleHandler(roleRepository);
        _getAllHandler = new GetAllHandler(roleRepository);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllCommand command)
    {
        var result = await _getAllHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRoleCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _createRoleHandler.Handle(command);

        return StatusCode(result.StatusCode, result);
    }
}
