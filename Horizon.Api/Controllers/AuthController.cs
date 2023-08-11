using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Route("v1")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser()
    {
        return Ok();
    }
}
