namespace Horizon.Domain.Queries.Responses.Roles;

public class GetDefaultRolesResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public bool IsDefault { get; set; }
}
