using Horizon.Shared.Commands;

namespace Horizon.Shared.Outputs;

public class CommandResult : ICommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public object? Data { get; set; }
    public object? Errors { get; set; }

    public CommandResult(bool success, string message, int statusCode, object? data = null, object? errors = null)
    {
        Success = success;
        Message = message;
        StatusCode = statusCode;
        Data = data;
        Errors = errors;
    }
}
