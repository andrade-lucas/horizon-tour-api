using FluentValidation;
using Horizon.Shared.Messages;
using Horizon.Domain.ValueObjects;

namespace Horizon.Domain.Validators.ValueObjects;

public class NameValidator : AbstractValidator<Name>
{
    public NameValidator()
    {
        RuleFor(name => name.FirstName)
            .NotEmpty().WithMessage(string.Format(Messages.Required, Fields.FirstName, 100))
            .MinimumLength(2).WithMessage(string.Format(Messages.MinLength, Fields.FirstName, 2))
            .MaximumLength(100).WithMessage(string.Format(Messages.MaxLength, Fields.FirstName, 100));

        RuleFor(name => name.LastName).MaximumLength(100).WithMessage(string.Format(Messages.MaxLength, Fields.LastName, 100));
        RuleFor(name => name.NickName).MaximumLength(50).WithMessage(string.Format(Messages.MaxLength, Fields.NickName, 50)); ;
    }
}
