using Horizon.Domain.ValueObjects;
using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities;

public class User : Entity
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public Password Password { get; private set; }
    public string? Image { get; private set; }
    public IList<Role> Roles { get; private set; }

    public User(Name name, Email email, Phone phone, Password password, string? image)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Password = password;
        Image = image;

        Roles = new List<Role>();
    }

    /// <summary>
    /// Add role to the user's roles list.
    /// </summary>
    /// <param name="role">
    public void AddRole(Role role)
    {
        if (role == null) return;

        Roles.Add(role);
    }

    public override string ToString()
    {
        return $"{Name.FirstName} {Name.LastName}";
    }
}
