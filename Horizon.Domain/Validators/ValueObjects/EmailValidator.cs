using FluentValidation;
using Horizon.Domain.ValueObjects;

namespace Horizon.Domain.Validators.ValueObjects;

public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(email => email.Address)
            .EmailAddress()
            .WithMessage("Invalid email address")
            .OverridePropertyName("Email");
    }
}
