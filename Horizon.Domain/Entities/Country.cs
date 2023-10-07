using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities
{
    public class Country : Entity
    {
        public string Name { get; private set; }
        public string Acronym { get; private set; }

        public Country(string name)
        {
            Name = name;
        }

        public void AddAcronym(string acronym) => Acronym = acronym;
    }
}
