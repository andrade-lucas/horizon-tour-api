using Horizon.Domain.Commands.Inputs.Account;
using Horizon.Domain.Repositories;
using Horizon.Domain.Services;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Account;

public class ChangeProfilePictureHandler : ICommandHandler<ChangeProfilePictureCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUploadImageService _uploadImageService;

    public ChangeProfilePictureHandler(IUserRepository userRepository, IUploadImageService uploadImageService)
    {
        _userRepository = userRepository;
        _uploadImageService = uploadImageService;
    }

    public async Task<ICommandResult> Handle(ChangeProfilePictureCommand command)
    {
        try
        {
            var imageName = $"profile_{Guid.NewGuid().ToString()}";
            var imageUri = await _uploadImageService.UploadBase64ImageAsync(
                command.ImageBase64, 
                "images/users", 
                imageName
            );

            await _userRepository.UploadProfileImageAsync(command.UserId, imageUri);

            return new CommandResult(true, string.Empty, (int)HttpStatusCode.OK, new
            {
                profileImageUrl = imageUri
            });
        }
        catch (Exception ex)
        {
            return new CommandResult
            {
                Success = false,
                Message = "Internal server error",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
