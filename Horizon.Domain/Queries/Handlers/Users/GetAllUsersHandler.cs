using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.Queries.Inputs.Users;
using Horizon.Domain.Repositories;
using Horizon.Shared.Contracts;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Queries.Handlers.Users;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IResult>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResult> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository.GetAllAsync(query.Filter, query.Page, query.PageSize);

            return new CommandResult(true, string.Empty, (int)HttpStatusCode.OK, users);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new CommandResult(false, PtBrMessages.Error, (int)HttpStatusCode.InternalServerError);
        }
    }
}
