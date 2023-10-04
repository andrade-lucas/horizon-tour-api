using Horizon.Shared.Queries;

namespace Horizon.Domain.Queries.Inputs.Users;

public class GetAllUsersQuery : IQuery
{
    public string? Filter { get; set; }
    public int page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
