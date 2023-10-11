using Microsoft.Extensions.DependencyInjection;

namespace Horizon.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddMediatR(conf => 
            conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
