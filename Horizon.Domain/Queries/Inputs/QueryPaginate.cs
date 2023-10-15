namespace Horizon.Domain.Queries.Inputs;

public record QueryPaginate(
    string? Filter = null,
    int Page = 0,
    int PageSize = 20
);
