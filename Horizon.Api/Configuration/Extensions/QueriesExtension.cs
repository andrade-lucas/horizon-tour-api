using Horizon.Domain.Queries.Handlers.Users;
using Horizon.Domain.Queries.Inputs.Users;
using Horizon.Shared.Queries;

namespace Horizon.Api.Configuration.Extensions;

public static class QueriesExtension
{
    public static void ConfigureQueries(this IServiceCollection services)
    {
        services.AddTransient<IQueryHandler<GetAllUsersQuery>, GetAllUsersHandler>();
    }
}
