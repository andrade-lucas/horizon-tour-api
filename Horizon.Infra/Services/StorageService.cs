using Azure.Storage.Blobs;
using Horizon.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace Horizon.Infra.Services
{
    public class StorageService : IStorageService
    {
        private readonly IConfiguration _configuration;

        public StorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteAsync(string containerName, string fileName)
        {
            try
            {
                var containerSplit = containerName.Split("/");
                var formatedPath = string.Empty;

                for (int i = 1; i <= containerSplit.Length - 1; i++) formatedPath += $"{containerSplit[i]}/";

                var blobConn = _configuration.GetValue<string>("AzureBlob");

                var blobClient = new BlobServiceClient(blobConn);
                var container = blobClient.GetBlobContainerClient(containerSplit[0]);

                var blob = container.GetBlobClient(formatedPath + fileName);

                var result = await blob.DeleteIfExistsAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
