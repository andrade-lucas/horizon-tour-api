using Microsoft.Extensions.DependencyInjection;

namespace Horizon.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddMediatR(conf => 
            conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
