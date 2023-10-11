using FluentValidation;
using Horizon.Domain.Commands.Inputs.Account;
using Horizon.Domain.Entities;
using Horizon.Shared.Messages;
using Horizon.Domain.Repositories;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Contracts;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Account;

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, IResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;

    public UpdateAccountHandler(IUserRepository userRepository, IValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }

    public async Task<IResult> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(command.Id);

            var name = new Name(command.FirstName, command.LastName, command.NickName);
            var email = new Email(user.Email);
            var entity = new User(command.Id, name, email, updatedAt: DateTime.Now);

            entity.AddBirthDate(command.Birthdate);

            if (command.Phone != null) entity.AddPhone(new Phone(command.Phone));

            var validate = await _userValidator.ValidateAsync(entity);
            if (!validate.IsValid)
                return new CommandResult(false, Messages.BadRequest, (int)HttpStatusCode.BadRequest, errors: validate.ToDictionary());

            await _userRepository.UpdateUserAsync(entity);

            return new CommandResult(true, string.Format(Messages.UpdatedSuccess, Fields.User), (int)HttpStatusCode.OK, data: command);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new CommandResult
            {
                Success = false,
                Message = Messages.Error,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
