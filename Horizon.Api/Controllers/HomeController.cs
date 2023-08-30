using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[AllowAnonymous]
public class HomeController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("health")]
    public IActionResult Index()
    {
        var version = _configuration.GetValue<string>("ApiVersion");

        return Ok(new
        {
            success = true,
            version = version
        });
    }
}
