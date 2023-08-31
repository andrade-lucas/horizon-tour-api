using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Responses.Account;

namespace Horizon.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();

    Task<GetCurrentUserResponse> GetByIdAsync(string id);

    Task UploadProfileImageAsync(string userId, string profileImageUrl);

    Task DeleteAsync(string id);
}
