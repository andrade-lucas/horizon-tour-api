namespace Horizon.Shared.Entities;

public record RequestResult
{
    public bool BadRequest { get; set; }
    public bool Unauthorized { get; set; }
    public bool NotFound { get; set; }
    public bool Success => !ErrorMessages.Any() || !BadRequest || !Unauthorized || !NotFound;
    public List<string> ErrorMessages { get; set; } = new List<string>();
    public List<string> SuccessMessages { get; set; } = new List<string>();
}

public record RequestResult<T> : RequestResult
{
    public required T Data { get; set; }
}