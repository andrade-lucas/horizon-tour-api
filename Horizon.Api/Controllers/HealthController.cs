using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[AllowAnonymous]
public class HealthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public HealthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("health")]
    public IActionResult Index()
    {
        var version = _configuration.GetValue<string>("ApiVersion");

        return Ok(new
        {
            Success = true,
            Version = version
        });
    }
}
