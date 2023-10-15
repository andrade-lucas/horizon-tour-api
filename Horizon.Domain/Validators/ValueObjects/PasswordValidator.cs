using FluentValidation;
using Horizon.Shared.Messages;
using Horizon.Domain.ValueObjects;

namespace Horizon.Domain.Validators.ValueObjects;

public class PasswordValidator : AbstractValidator<Password>
{
    public PasswordValidator()
    {
        RuleFor(pass => pass.Value)
            .NotEmpty().WithMessage(string.Format(Messages.Required, Fields.Password))
            .MinimumLength(4).WithMessage(string.Format(Messages.MinLength, Fields.Password, 4))
            .MaximumLength(512).WithMessage(string.Format(Messages.MaxLength, Fields.Password, 512))
            .OverridePropertyName("Password");
    }
}
