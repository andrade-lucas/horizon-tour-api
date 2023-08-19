using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

public class HomeController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Index()
    {
        return Ok(new
        {
            success = true,
            version = "1.0.0"
        });
    }
}
