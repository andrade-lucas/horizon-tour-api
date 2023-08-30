using Horizon.Domain.ValueObjects;
using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities;

public class User : Entity
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone? Phone { get; private set; }
    public Password Password { get; private set; }
    public string? ImageUrl { get; private set; }
    public bool Verified { get; private set; }
    public DateTime? Birthdate { get; private set; }
    public List<Role> Roles { get; private set; } = new List<Role>();

    public User(Name name, Email email,Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public User(string id, Name name, Email email, Password password, string? image) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        ImageUrl = image;
    }

    /// <summary>
    /// Add role range to the user's roles list.
    /// </summary>
    /// <param name="role">
    public void AddRoleRange(IEnumerable<Role> roles)
    {
        if (roles == null || roles.Count() == 0) return;

        Roles.AddRange(roles);
    }

    public override string ToString()
    {
        return $"{Name.FirstName} {Name.LastName}";
    }
}
