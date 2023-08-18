using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities;

public class Role : Entity
{
    public string Slug { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
