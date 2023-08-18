using System.ComponentModel.DataAnnotations;

namespace Horizon.Domain.ValueObjects;

public class Password
{
    [Required]
    [MinLength(6)]
    public string Value { get; private set; }

    public Password(string value)
    { 
        this.Value = value;
    }
}
