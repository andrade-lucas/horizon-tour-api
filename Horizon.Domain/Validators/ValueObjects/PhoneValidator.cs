using FluentValidation;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace Horizon.Domain.Validators.ValueObjects;

public class PhoneValidator : AbstractValidator<Phone>
{
    public PhoneValidator()
    {
        RuleFor(phone => phone.Number)
            .Matches(new Regex("^[0-9]"))
            .WithMessage(string.Format(PtBrMessages.InvalidField, PtBrFields.PhoneNumber))
            .OverridePropertyName("PhoneNumber");
    }
}
