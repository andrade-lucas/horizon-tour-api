﻿using Horizon.Auth.Command.Inputs;
using Horizon.Auth.Repositories;
using Horizon.Auth.Services.Contracts;
using Horizon.Auth.Queries.Responses;
using Horizon.Domain.Repositories;
using Horizon.Domain.Security;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Outputs;
using System.Net;
using FluentValidation;
using Horizon.Shared.Messages;
using MediatR;
using Horizon.Shared.Contracts;

namespace Horizon.Auth.Command.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, IResult>
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokenService _tokenService;
    private readonly IRoleRepository _roleRepository;
    private readonly IValidator<Email> _emailValidator;
    private readonly IValidator<Password> _passwordValidator;

    public LoginHandler(
        IAuthRepository authRepository, 
        IRoleRepository roleRepository, 
        ITokenService tokenService,
        IValidator<Email> emailValidator,
        IValidator<Password> passwordValidator
     )
    {
        _authRepository = authRepository;
        _tokenService = tokenService;
        _roleRepository = roleRepository;
        _emailValidator = emailValidator;
        _passwordValidator = passwordValidator;
    }

    public async Task<IResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var email = new Email(command.Email);
            var password = new Password(command.Password);

            var emailValidations = await _emailValidator.ValidateAsync(email);
            var passValidations = await _passwordValidator.ValidateAsync(password);

            if (!emailValidations.IsValid || !passValidations.IsValid)
            {
                var errors = emailValidations.ToDictionary()
                    .Concat(passValidations.ToDictionary())
                    .ToDictionary(x => x.Key, x=> x.Value);

                return new CommandResult(false, string.Format(Messages.BadRequest, Fields.User), (int)HttpStatusCode.BadRequest, errors: errors);
            }

            var user = await _authRepository.GetByEmailAsync(email.Address);

            if (user == null)
                return new CommandResult(false, string.Format(Messages.NotFound, Fields.User), (int)HttpStatusCode.BadRequest);

            var verifiedPass = PasswordHasherSecurity.Verify(password.Value, user.Password.Value);

            if (!verifiedPass)
                return new CommandResult(false, string.Format(Messages.NotFound, Fields.User), (int)HttpStatusCode.BadRequest);

            var roles = await _roleRepository.GetByUserAsync(user.Id.ToString());

            if (roles != null) user.AddRoleRange(roles);

            var token = _tokenService.GenerateToken(user);

            return new CommandResult(true, string.Empty, (int)HttpStatusCode.OK, new 
            { 
                token,
                user = new GetUserAuthResponse
                {
                    Id = user.Id.ToString(),
                    Name = $"{user.Name.FirstName} {user.Name.LastName}",
                    NickName = user.Name.NickName,
                    Email = user.Email.ToString(),
                    ProfileImageUrl = user.ProfileImageUrl,
                    Verified = user.Verified
                }
            });
        }
        catch (Exception ex)
        {
            return new CommandResult(false, Messages.Error, (int)HttpStatusCode.InternalServerError, errors: ex.Message);
        }
    }
}
