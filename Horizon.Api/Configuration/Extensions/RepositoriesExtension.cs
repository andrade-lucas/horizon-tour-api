﻿using Horizon.Auth.Repositories;
using Horizon.Domain.Repositories;
using Horizon.Infra.Repositories;

namespace Horizon.Api.Configuration.Extensions;

public static class RepositoriesExtension
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddTransient<IAuthRepository, AuthRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IUsersRolesRepository, UsersRolesRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}
