namespace Horizon.Domain.Services;

public interface IStorageService
{
    Task DeleteAsync(string containerName, string fileName);
}
