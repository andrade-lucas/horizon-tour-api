using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities;

public class Role : Entity
{

    public string Name { get; private set; }
    public string Slug { get; private set; }
    public bool IsDefault { get; private set; }

    public Role(string name, string slug, bool isDefault = false)
    {
        Name = name;
        Slug = slug;
        IsDefault = isDefault;
    }

    public Role(string id, string name, string slug, bool isDefault = false) : base(id)
    {
        Name = name;
        Slug = slug;
        IsDefault = isDefault;
    }

    public Role(string id, string name, string slug, DateTime createdAt, DateTime updatedAt, bool isDefault = false)
        : base(id, createdAt, updatedAt)
    {
        Name = name;
        Slug = slug;
        IsDefault = isDefault;
    }
}
