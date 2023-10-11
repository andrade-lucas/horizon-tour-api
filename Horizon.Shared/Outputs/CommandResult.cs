using Horizon.Shared.Contracts;

namespace Horizon.Shared.Outputs;

public class CommandResult : IResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public object? Data { get; set; }
    public object? Errors { get; set; }

    public CommandResult() { }

    public CommandResult(bool success, string message, int statusCode, object? data = null, object? errors = null)
    {
        Success = success;
        Message = message;
        StatusCode = statusCode;
        Data = data;
        Errors = errors;
    }
}
