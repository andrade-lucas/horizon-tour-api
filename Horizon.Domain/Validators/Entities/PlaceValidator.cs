using FluentValidation;
using Horizon.Domain.Entities;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.Validators.ValueObjects;

namespace Horizon.Domain.Validators.Entities;

public class PlaceValidator : AbstractValidator<Place>
{
    public PlaceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(string.Format(PtBrMessages.Required, "Nome"))
            .MinimumLength(5).WithMessage(string.Format(PtBrMessages.MinLength, "Nome", 5))
            .MaximumLength(256).WithMessage(string.Format(PtBrMessages.MaxLength, "Nome", 256));

        RuleFor(x => x.Owner).NotNull().WithMessage(string.Format(PtBrMessages.Required, "Proprietário")).OverridePropertyName("OwnerId");
        RuleFor(x => x.Type).NotEmpty().WithMessage(string.Format(PtBrMessages.Required, "Tipo"));
        RuleFor(x => x.Address).SetValidator(new AddressValidator())
            .When(x => x.Address != null);
    }
}
