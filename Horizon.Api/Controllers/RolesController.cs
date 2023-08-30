using Horizon.Api.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horizon.Api.Controllers;

//[Authorize(Roles = "god")]
public class RolesController : ControllerBase
{
    public async Task<IActionResult> Get()
    {
        return Ok();
    }

    public async Task<IActionResult> Create(CreateRoleViewModel model)
    {
        if (! ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return StatusCode((int)HttpStatusCode.Created, new
        {
            success = true,
            message = "Role created with success"
        });
    }
}
