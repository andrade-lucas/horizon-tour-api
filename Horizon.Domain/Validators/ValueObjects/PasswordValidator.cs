using FluentValidation;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.ValueObjects;

namespace Horizon.Domain.Validators.ValueObjects;

public class PasswordValidator : AbstractValidator<Password>
{
    public PasswordValidator()
    {
        RuleFor(pass => pass.Value)
            .NotEmpty().WithMessage(string.Format(PtBrMessages.Required, PtBrFields.Password))
            .MinimumLength(4).WithMessage(string.Format(PtBrMessages.MinLength, PtBrFields.Password, 4))
            .MaximumLength(512).WithMessage(string.Format(PtBrMessages.MaxLength, PtBrFields.Password, 512))
            .OverridePropertyName("Password");
    }
}
