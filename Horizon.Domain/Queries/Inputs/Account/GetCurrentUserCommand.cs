using Horizon.Shared.Commands;

namespace Horizon.Domain.Queries.Inputs.Account;

public class GetCurrentUserCommand : ICommand
{
    public string UserId { get; set; }
}
