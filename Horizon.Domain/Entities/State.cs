using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities;

public class State : Entity
{
    public string Name { get; private set; }
    public string UF { get; private set; }
    public Country Country { get; private set; }

    public State(string name, string uf)
    {
        Name = name;
        UF = uf;
    }

    public State(string id, string name, string uf) : base(id)
    {
        Name = name;
        UF = uf;
    }

    public void AddCountry(Country country) => Country = country;
}
