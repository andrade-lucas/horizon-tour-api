using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities;

public class Role : Entity
{
    public string Name { get; private set; }
    public string Slug { get; private set; }
}
