using FluentValidation;
using Horizon.Domain.Entities;
using Horizon.Domain.Validators.ValueObjects;

namespace Horizon.Domain.Validators.Entities;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Name).SetValidator(new NameValidator());
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator())
            .When(user => user.Password != null);

        RuleFor(user => user.Phone)
            .SetValidator(new PhoneValidator())
            .When(user => user.Phone != null);
    }
}
