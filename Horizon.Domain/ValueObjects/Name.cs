using System.ComponentModel.DataAnnotations;

namespace Horizon.Domain.ValueObjects;

public class Name
{
    [Required]
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    [Required]
    public string NickName { get; private set; }

    public Name(string firstName, string lastName, string nickName)
    {
        FirstName = firstName;
        LastName = lastName;
        NickName = nickName;
    }
}
