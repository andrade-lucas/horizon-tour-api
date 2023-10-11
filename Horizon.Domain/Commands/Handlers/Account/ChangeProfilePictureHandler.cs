using Horizon.Domain.Commands.Inputs.Account;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.Repositories;
using Horizon.Domain.Services;
using Horizon.Shared.Contracts;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Account;

public class ChangeProfilePictureHandler : IRequestHandler<ChangeProfilePictureCommand, IResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IUploadImageService _uploadImageService;
    private readonly IStorageService _storageService;

    public ChangeProfilePictureHandler(IUserRepository userRepository, IUploadImageService uploadImageService, IStorageService storageService)
    {
        _userRepository = userRepository;
        _uploadImageService = uploadImageService;
        _storageService = storageService;
    }

    public async Task<IResult> Handle(ChangeProfilePictureCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var container = "images/users";

            var imageName = $"profile_{Guid.NewGuid().ToString()}";
            var imageUri = await _uploadImageService.UploadBase64ImageAsync(
                command.ImageBase64,
                container, 
                imageName
            );

            var currentProfileImage = await _userRepository.GetCurrentUserProfileUrl(command.UserId);
            await _userRepository.UploadProfileImageAsync(command.UserId, imageUri);

            if (currentProfileImage != null)
            {
                var pathSplited = currentProfileImage.Split("/");
                await _storageService.DeleteAsync(container, pathSplited[pathSplited.Length - 1]);
            }

            return new CommandResult(true, string.Format(PtBrMessages.UpdatedSuccess, PtBrFields.ProfileImage), (int)HttpStatusCode.OK, new
            {
                profileImageUrl = imageUri
            });
        }
        catch (Exception ex)
        {
            return new CommandResult
            {
                Success = false,
                Message = PtBrMessages.Error,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
