using FluentValidation;
using Horizon.Domain.Validators.Entities;
using Horizon.Domain.Validators.ValueObjects;

namespace Horizon.Api.Configuration.Extensions
{
    public static class ValidationsExtension
    {
        public static void ConfigureValidations(this IServiceCollection services)
        {
            // Entities.
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddValidatorsFromAssemblyContaining<PlaceValidator>();

            // Value objects.
            services.AddValidatorsFromAssemblyContaining<EmailValidator>();
            services.AddValidatorsFromAssemblyContaining<NameValidator>();
            services.AddValidatorsFromAssemblyContaining<PasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<PhoneValidator>();
            services.AddValidatorsFromAssemblyContaining<AddressValidator>();
        }
    }
}
