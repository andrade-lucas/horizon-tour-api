﻿using FluentValidation;
using Horizon.Domain.Commands.Inputs.Account;
using Horizon.Domain.Entities;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.Repositories;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Account;

public class UpdateAccountHandler : ICommandHandler<UpdateAccountCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;

    public UpdateAccountHandler(IUserRepository userRepository, IValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }

    public async Task<ICommandResult> Handle(UpdateAccountCommand command)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(command.Id);

            var name = new Name(command.FirstName, command.LastName, command.NickName);
            var email = new Email(user.Email);
            var phone = new Phone(command.Phone);
            var entity = new User(command.Id, name, email, updatedAt: DateTime.Now);
            entity.AddPhone(phone);
            entity.AddBirthDate(command.Birthdate);

            var validate = await _userValidator.ValidateAsync(entity);
            if (!validate.IsValid)
                return new CommandResult(false, PtBrMessages.BadRequest, (int)HttpStatusCode.BadRequest, errors: validate.ToDictionary());

            await _userRepository.UpdateUserAsync(entity);

            return new CommandResult(true, string.Format(PtBrMessages.UpdatedSuccess, PtBrFields.User), (int)HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new CommandResult
            {
                Success = false,
                Message = PtBrMessages.Error,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
