using Horizon.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Api.Helpers;

public static class ControllerHelper
{
    public static IActionResult ParseToActionResult(this RequestResult requestResult)
    {
        var errorMessage = string.Join(Environment.NewLine, requestResult.ErrorMessages);
        if (requestResult.BadRequest)
        {
            return errorMessage.Any() ? new BadRequestObjectResult(errorMessage) : new BadRequestResult();
        }

        if (requestResult.Unauthorized)
        {
            return errorMessage.Any() ? new UnauthorizedObjectResult(errorMessage) : new UnauthorizedResult();
        }

        if (requestResult.NotFound)
        {
            return errorMessage.Any() ? new NotFoundObjectResult(errorMessage) : new NotFoundResult();
        }

        var successMessage = string.Join(Environment.NewLine, requestResult.SuccessMessages);
        return successMessage.Any() ? new OkObjectResult(successMessage) : new OkResult();
    }

    public static IActionResult ParseToActionResultData<T>(this RequestResult<T> requestResult)
    {
        return requestResult.Success ? new OkObjectResult(requestResult.Data) : ParseToActionResult(requestResult);
    }
}