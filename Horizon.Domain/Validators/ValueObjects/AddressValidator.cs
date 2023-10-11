using FluentValidation;
using Horizon.Shared.Messages;
using Horizon.Domain.ValueObjects;

namespace Horizon.Domain.Validators.ValueObjects;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.LatLong.Latitude)
            .NotNull().WithMessage(string.Format(Messages.Required, "Latitude")).OverridePropertyName("Latitude");
        RuleFor(x => x.LatLong.Longitude)
            .NotNull().WithMessage(string.Format(Messages.Required, "Longitude")).OverridePropertyName("Longitude");
        RuleFor(x => x.ZipCode)
            .NotNull().WithMessage(string.Format(Messages.Required, "Cep")).OverridePropertyName("ZipCode");
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage(string.Format(Messages.Required, "Logradouro")).OverridePropertyName("Street");
        RuleFor(x => x.Neighborhood)
            .NotEmpty().WithMessage(string.Format(Messages.Required, "Bairro")).OverridePropertyName("Neighborhood");
        RuleFor(x => x.City)
            .NotEmpty().WithMessage(string.Format(Messages.Required, "Cidade")).OverridePropertyName("City");
    }
}
