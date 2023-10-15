using Microsoft.OpenApi.Models;

namespace Horizon.Api.Extensions;

public static class Swagger
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(conf => 
        {
            conf.SwaggerDoc("v1", new OpenApiInfo { Title = "Horizon Tour API", Version = "v1" });
            conf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                    "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                    "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });
            conf.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}
