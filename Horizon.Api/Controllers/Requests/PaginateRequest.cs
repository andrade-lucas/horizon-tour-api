namespace Horizon.Api.Controllers.Requests;

public record PaginateRequest(
    string? Filter = null,
    int Page = 0,
    int PageSize = 20
);
