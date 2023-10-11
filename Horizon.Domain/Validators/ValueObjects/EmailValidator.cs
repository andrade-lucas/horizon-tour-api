using FluentValidation;
using Horizon.Shared.Messages;
using Horizon.Domain.ValueObjects;

namespace Horizon.Domain.Validators.ValueObjects;

public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(email => email.Address)
            .EmailAddress()
            .WithMessage(string.Format(Messages.InvalidField, Fields.Email))
            .OverridePropertyName("Email");
    }
}
