using Horizon.Shared.Commands;

namespace Horizon.Domain.Commands.Inputs.Account;

public class ChangeProfilePictureCommand : ICommand
{
    public string UserId { get; set; }
    public string ImageBase64 { get; set; }
}
