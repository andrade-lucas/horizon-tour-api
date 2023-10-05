using FluentValidation;
using Horizon.Auth.Command.Inputs;
using Horizon.Auth.Repositories;
using Horizon.Auth.Services.Contracts;
using Horizon.Domain.Entities;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.Repositories;
using Horizon.Domain.Security;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Horizon.Auth.Command.Handlers;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
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

    public async Task<ICommandResult> Handle(RegisterUserCommand command)
    {
        var passHash = PasswordHasherSecurity.Hash(command.Password);

        var name = new Name(command.FirstName, command.LastName, command.NickName);
        var email = new Email(command.Email);
        var password = new Password(passHash);

        var user = new User(name, email, password);

        var userValidator = await _validator.ValidateAsync(user);

        if (!userValidator.IsValid)
            return new CommandResult(false, PtBrMessages.BadRequest, (int)HttpStatusCode.BadRequest, errors: userValidator.ToDictionary());

        var emailExists = await _authRepository.EmailExistsAsync(email);
        if (emailExists)
            return new CommandResult(false, PtBrMessages.EmailExists, (int)HttpStatusCode.BadRequest);

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
            return new CommandResult(false, PtBrMessages.Error, (int)HttpStatusCode.InternalServerError, errors: e.Message);
        }
    }
}
