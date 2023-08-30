using Horizon.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Auth.Command.Inputs;

public class RegisterUserCommand : ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
