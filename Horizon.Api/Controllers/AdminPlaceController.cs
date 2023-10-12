using Horizon.Api.Controllers.Requests;
using Horizon.Api.Controllers.Requests.Places;
using Horizon.Domain.Commands.Inputs.Places;
using Horizon.Domain.Queries.Inputs.Places;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Route("v1/admin-places")]
[Authorize(Roles = "admin, manager")]
public class AdminPlaceController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminPlaceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] PaginateRequest request)
    {
        var query = new GetPlacesQuery(
            User.FindFirst("id")?.Value,
            request.Filter,
            request.Page,
            request.PageSize
        );

        var result = await _mediator.Send(query);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlaceRequest request)
    {
        var command = new CreatePlaceCommand(
            request.Name,
            User.FindFirst("id")?.Value,
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
