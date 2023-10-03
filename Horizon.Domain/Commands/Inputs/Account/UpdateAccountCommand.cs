using Horizon.Shared.Commands;

namespace Horizon.Domain.Commands.Inputs.Account;

public class UpdateAccountCommand : ICommand
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public string Phone { get; set; }
    public DateTime Birthdate { get; set; }
}
