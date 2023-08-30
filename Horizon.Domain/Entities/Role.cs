using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities;

public class Role : Entity
{

    public string Name { get; private set; }
    public string Slug { get; private set; }

    public Role(string name, string slug)
    {
        Name = name;
        Slug = slug;
    }

    public Role(string id, string name, string slug) : base(id)
    {
        Name = name;
        Slug = slug;
    }
}
