using Horizon.Api.ViewModels.Auth;
using Horizon.Auth.Repositories;
using Horizon.Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Route("v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthRepository _authRepository;
    
    public AuthController() 
    {
        _authRepository = new AuthRepository();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody]LoginViewModel model
    )
    {
        var user = _authRepository.GetByCredentials(model.Email, model.Password);

        if (user == null) return NotFound(new
        {
            success = false,
            message = "email or password is incorrect"
        });

        var token = TokenService.GenerateToken(user);

        return Ok(new 
        {
            success = true,
            token
        });
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser()
    {
        return Ok();
    }
}
