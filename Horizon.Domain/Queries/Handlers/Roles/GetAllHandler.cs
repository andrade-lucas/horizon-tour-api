using Horizon.Shared.Messages;
using Horizon.Domain.Queries.Inputs.Roles;
using Horizon.Domain.Repositories;
using Horizon.Shared.Contracts;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Queries.Handlers.Roles;

public class GetAllHandler : IRequestHandler<GetRolesQuery, IResult>
{
    private readonly IRoleRepository _repository;

    public GetAllHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(GetRolesQuery query, CancellationToken cancellationToken)
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
                Message = Messages.Error,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Errors = ex.Message
            };
        }
    }
}
