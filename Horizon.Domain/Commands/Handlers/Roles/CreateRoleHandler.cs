﻿using Horizon.Domain.Commands.Inputs.Roles;
using Horizon.Domain.Entities;
using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.Repositories;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Roles;

public class CreateRoleHandler : ICommandHandler<CreateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ICommandResult> Handle(CreateRoleCommand command)
    {
        try
        {
            var role = new Role(command.Name, command.Slug);

            await _roleRepository.CreateAsync(role);

            return new CommandResult(true, string.Format(PtBrMessages.UpdatedSuccess, PtBrFields.Role), (int)HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return new CommandResult
            {
                Success = false,
                Message = PtBrMessages.Error,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Errors = ex.Message
            };
        }
    }
}
