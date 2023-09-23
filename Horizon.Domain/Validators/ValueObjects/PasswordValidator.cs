using FluentValidation;
using Horizon.Domain.ValueObjects;

namespace Horizon.Domain.Validators.ValueObjects;

public class PasswordValidator : AbstractValidator<Password>
{
    public PasswordValidator()
    {
        RuleFor(pass => pass.Value)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(512)
            .OverridePropertyName("Password");
    }
}
