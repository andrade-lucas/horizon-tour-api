using Horizon.Domain.Queries.Inputs.Roles;
using Horizon.Domain.Repositories;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Domain.Queries.Handlers.Roles;

public class GetAllHandler : ICommandHandler<GetAllCommand>
{
    private readonly IRoleRepository _repository;

    public GetAllHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> Handle(GetAllCommand command)
    {
        try
        {
            var result = await _repository.GetAllAsync();

            return new CommandResult(true, string.Empty, (int)HttpStatusCode.OK, result);
        }
        catch (Exception ex)
        {
            return new CommandResult
            {
                Success = false,
                Message = "Internal server error",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Errors = ex.Message
            };
        }
    }
}
