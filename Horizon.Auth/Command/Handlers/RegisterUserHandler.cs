using FluentValidation;
using Horizon.Auth.Command.Inputs;
using Horizon.Auth.Repositories;
using Horizon.Auth.Services.Contracts;
using Horizon.Domain.Entities;
using Horizon.Shared.Messages;
using Horizon.Domain.Repositories;
using Horizon.Domain.Security;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Contracts;
using Horizon.Shared.Outputs;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Horizon.Auth.Command.Handlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IResult>
{
    private readonly IAuthRepository _authRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly IValidator<User> _validator;

    public RegisterUserHandler(
        IAuthRepository authRepository,
        IRoleRepository roleRepository,
        ITokenService tokenService,
        IConfiguration configuration,
        IValidator<User> validator
     )
    {
        _authRepository = authRepository;
        _roleRepository = roleRepository;
        _tokenService = tokenService;
        _configuration = configuration;
        _validator = validator;
    }

    public async Task<IResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var passHash = PasswordHasherSecurity.Hash(command.Password);

        var name = new Name(command.FirstName, command.LastName, command.NickName);
        var email = new Email(command.Email);
        var password = new Password(passHash);

        var user = new User(name, email, password);

        var userValidator = await _validator.ValidateAsync(user);

        if (!userValidator.IsValid)
            return new CommandResult(false, Messages.BadRequest, (int)HttpStatusCode.BadRequest, errors: userValidator.ToDictionary());

        var emailExists = await _authRepository.EmailExistsAsync(email);
        if (emailExists)
            return new CommandResult(false, Messages.EmailExists, (int)HttpStatusCode.BadRequest);

        var defaultRoles = await _roleRepository.GetDefaultAsync();

        if (defaultRoles != null && defaultRoles.Count() > 0)
            user.AddRoleRange(defaultRoles);

        try
        {
            await _authRepository.CreateAsync(user);
            var token = _tokenService.GenerateToken(user);

            return new CommandResult(
                true, 
                string.Empty, 
                (int)HttpStatusCode.Created,
                token
            );
        }
        catch (Exception e)
        {
            return new CommandResult(false, Messages.Error, (int)HttpStatusCode.InternalServerError, errors: e.Message);
        }
    }
}
