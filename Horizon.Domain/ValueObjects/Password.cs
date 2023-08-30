using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Horizon.Domain.ValueObjects;

public class Password
{
    public string Value { get; private set; }

    public Password(string value)
    {
        Value = value;
    }
}
