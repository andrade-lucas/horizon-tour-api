﻿using FluentValidation;
using Horizon.Shared.Messages;
using Horizon.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace Horizon.Domain.Validators.ValueObjects;

public class PhoneValidator : AbstractValidator<Phone>
{
    public PhoneValidator()
    {
        RuleFor(phone => phone.Number)
            .Matches(new Regex("^[0-9]"))
            .WithMessage(string.Format(Messages.InvalidField, Fields.PhoneNumber))
            .OverridePropertyName("PhoneNumber");
    }
}
