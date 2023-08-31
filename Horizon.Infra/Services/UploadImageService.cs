using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Horizon.Domain.Services;
using Horizon.Shared.Helpers;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace Horizon.Infra.Services;

public class UploadImageService : IUploadImageService
{
    private readonly IConfiguration _configuration;

    public UploadImageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> UploadBase64ImageAsync(string base64Image, string container, string imageName)
    {
        var blobConnection = _configuration.GetConnectionString("AzureBlob");

        var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");
        var fileExtension = GetFileExtension.FromBase64String(data);

        byte[] imageBytes = Convert.FromBase64String(data);

        var blobClient = new BlobClient(blobConnection, container, $"{imageName}.{fileExtension}");

        BlobHttpHeaders headers = new BlobHttpHeaders();
        headers.ContentType = $"image/{fileExtension}";

        using (var stream = new MemoryStream(imageBytes))
        {
            await blobClient.UploadAsync(stream, headers);
        }

        return blobClient.Uri.AbsoluteUri;
    }
}
