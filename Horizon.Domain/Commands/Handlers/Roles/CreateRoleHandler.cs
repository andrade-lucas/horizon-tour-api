using Horizon.Domain.Commands.Inputs.Roles;
using Horizon.Domain.Entities;
using Horizon.Shared.Messages;
using Horizon.Domain.Repositories;
using Horizon.Shared.Contracts;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Roles;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, IResult>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IResult> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var role = new Role(command.Name, command.Slug);

            await _roleRepository.CreateAsync(role);

            return new CommandResult(true, string.Format(Messages.UpdatedSuccess, Fields.Role), (int)HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return new CommandResult
            {
                Success = false,
                Message = Messages.Error,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Errors = ex.Message
            };
        }
    }
}
