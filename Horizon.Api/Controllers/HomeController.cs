using Horizon.Api.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

public class HomeController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Index()
    {
        var version = AppSettings.ApiVersion;

        return Ok(new
        {
            success = true,
            version = version
        });
    }
}
