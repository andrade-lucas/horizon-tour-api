using Horizon.Api.Controllers.Requests.Places;
using Horizon.Domain.Commands.Inputs.Places;
using Horizon.Shared.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Controllers;

[Route("v1/places")]
[Authorize(Roles = "admin, manager")]
public class PlaceController : ControllerBase
{
    private readonly ICommandHandler<CreatePlaceCommand> _createPlaceCommand;

    public PlaceController(ICommandHandler<CreatePlaceCommand> createPlaceCommand)
    {
        _createPlaceCommand = createPlaceCommand;
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
        var command = new CreatePlaceCommand();
        command.Name = request.Name;
        command.OwnerId = User.FindFirst("id")?.Value;
        command.CityId = request.CityId;
        command.Street = request.Street;
        command.Number = request.Number;
        command.Neighborhood = request.Neighborhood;
        command.ZipCode = request.ZipCode;
        command.Latitude = request.Latitude;
        command.Longitude = request.Longitude;
        command.PresentationImageBase64 = request.PresentationImageBase64;
        command.AutomaticOpen = request.AutomaticOpen;
        command.Status = request.Status;
        command.Description = request.Description;
        command.Type = request.Type;

        var result = await _createPlaceCommand.Handle(command);

        return StatusCode(result.StatusCode, result);
    }
}
