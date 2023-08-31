namespace Horizon.Domain.Services;

public interface IUploadImageService
{
    Task<string> UploadBase64ImageAsync(string base64Image, string container, string imageName);
}
