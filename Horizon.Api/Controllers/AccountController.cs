using Horizon.Api.Controllers.Requests.Account;
using Horizon.Domain.Commands.Inputs.Account;
using Horizon.Domain.Queries.Inputs.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Authorize]
[Route("v1/account")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentAsync()
    {
        var command = new GetCurrentUserQuery(User.FindFirst("id")?.Value);

        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAccountRequest request)
    {
        var command = new UpdateAccountCommand(
            User.FindFirst("id")?.Value,
            request.FirstName,
            request.LastName,
            request.NickName,
            request.Phone,
            request.Birthdate
        );

        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPatch("change-profile-picture")]
    public async Task<IActionResult> ChangeProfilePicture([FromBody] ChangeProfilePictureRequest request)
    {
        var command = new ChangeProfilePictureCommand(User.FindFirst("id")?.Value, request.ImageBase64);

        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }
}
