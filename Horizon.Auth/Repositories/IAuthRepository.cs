using Horizon.Domain.Entities;
using Horizon.Domain.ValueObjects;

namespace Horizon.Auth.Repositories;

public interface IAuthRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task CreateAsync(User user);
}
