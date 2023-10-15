using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Responses.Account;
using Horizon.Domain.Queries.Responses.Users;
using Horizon.Shared.Outputs;

namespace Horizon.Domain.Repositories;

public interface IUserRepository
{
    Task<PaginationResult<GetAllUsersResponse>> GetAllAsync(string? filter, int page = 0, int pageSize = 20);

    Task<GetCurrentUserResponse> GetByIdAsync(string id);

    Task UploadProfileImageAsync(string userId, string profileImageUrl);

    Task<string?> GetCurrentUserProfileUrl(string userId);

    Task UpdateUserAsync(User user);

    Task DeleteAsync(string id);
}
