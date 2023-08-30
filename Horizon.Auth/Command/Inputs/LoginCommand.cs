using Horizon.Shared.Commands;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Auth.Command.Inputs;

public class LoginCommand : ICommand
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
