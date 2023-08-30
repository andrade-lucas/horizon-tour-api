using Microsoft.Extensions.Configuration;

namespace Horizon.Api.Settings;

public static class AppSettings
{
    private static IConfiguration _configuration;

    public static string? ApiVersion { get; set; }
    public static string? JwtSecret { get; set; }
    public static ConnectionStrings? ConnectionStrings { get; set; }

    public static void Initialize(IConfiguration configuration)
    {
        _configuration = configuration;

        Bind();
    }

    private static void Bind()
    {
        ApiVersion = _configuration.GetValue<string>("ApiVersion");
        JwtSecret = _configuration.GetValue<string>("JwtSecret");
        ConfigureConnectionStrings();
    }

    private static void ConfigureConnectionStrings()
    {
        var connectionStrings = new ConnectionStrings();
        _configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
        ConnectionStrings = connectionStrings;
    }
}
