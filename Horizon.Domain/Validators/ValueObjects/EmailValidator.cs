using FluentValidation;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.ValueObjects;

namespace Horizon.Domain.Validators.ValueObjects;

public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(email => email.Address)
            .EmailAddress()
            .WithMessage(string.Format(PtBrMessages.InvalidField, PtBrFields.Email))
            .OverridePropertyName("Email");
    }
}
