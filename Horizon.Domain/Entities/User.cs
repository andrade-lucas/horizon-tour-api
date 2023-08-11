using Horizon.Domain.ValueObjects;
using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities;

public class User : Entity
{
    public Name Name { get; private set; }
}
