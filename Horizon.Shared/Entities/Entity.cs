namespace Horizon.Shared.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public Entity(string id)
    {
        Id = new Guid(id);
    }
}
