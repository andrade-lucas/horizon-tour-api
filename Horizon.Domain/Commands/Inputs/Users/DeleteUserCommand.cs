using Horizon.Shared.Commands;

namespace Horizon.Domain.Commands.Inputs.Users;

public class DeleteUserCommand : ICommand
{
    public string UserId { get; set; }
}
