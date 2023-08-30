namespace Horizon.Shared.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Entity(string id, DateTime? createdAt = null, DateTime? updatedAt = null, DateTime? deletedAt = null)
    {
        Id = new Guid(id);
        CreatedAt = createdAt ?? DateTime.Now;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }
}
