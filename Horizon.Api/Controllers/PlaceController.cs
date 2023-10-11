using Horizon.Api.Controllers.Requests.Places;
using Horizon.Domain.Commands.Inputs.Places;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Route("v1/places")]
[Authorize(Roles = "admin, manager")]
public class PlaceController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlaceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return StatusCode(200, "PlaceController.Index");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlaceRequest request)
    {
        var command = new CreatePlaceCommand(
            request.Name,
            User.FindFirst("id")?.Value,
            request.status,
            request.CityId,
            request.Street,
            request.Number,
            request.ZipCode,
            request.Neighborhood,
            request.Latitude,
            request.Longitude,
            request.AutomaticOpen,
            request.Type,
            request.Description,
            request.PresentationImageBase64
        );

        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }
}
