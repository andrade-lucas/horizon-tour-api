namespace Horizon.Domain.Queries.Responses.Auth;

public class GetRolesResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
