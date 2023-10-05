using FluentValidation;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.ValueObjects;

namespace Horizon.Domain.Validators.ValueObjects;

public class NameValidator : AbstractValidator<Name>
{
    public NameValidator()
    {
        RuleFor(name => name.FirstName)
            .NotEmpty().WithMessage(string.Format(PtBrMessages.Required, PtBrFields.FirstName, 100))
            .MinimumLength(2).WithMessage(string.Format(PtBrMessages.MinLength, PtBrFields.FirstName, 2))
            .MaximumLength(100).WithMessage(string.Format(PtBrMessages.MaxLength, PtBrFields.FirstName, 100));

        RuleFor(name => name.LastName).MaximumLength(100).WithMessage(string.Format(PtBrMessages.MaxLength, PtBrFields.LastName, 100));
        RuleFor(name => name.NickName).MaximumLength(50).WithMessage(string.Format(PtBrMessages.MaxLength, PtBrFields.NickName, 50)); ;
    }
}
