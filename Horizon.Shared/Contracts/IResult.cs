namespace Horizon.Shared.Contracts;

public interface IResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public object? Data { get; set; }
    public object? Errors { get; set; }
}
