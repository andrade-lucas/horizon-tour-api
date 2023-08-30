using Horizon.Shared.Commands;

namespace Horizon.Domain.Commands.Inputs.Roles;

public class CreateRoleCommand : ICommand
{
    public string Name { get; set; }
    public string Slug { get; set; }
}
