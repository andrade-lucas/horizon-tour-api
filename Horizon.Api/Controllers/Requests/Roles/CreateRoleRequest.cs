namespace Horizon.Api.Controllers.Requests.Roles;

public record CreateRoleRequest(
    string Name,
    string Slug
);
