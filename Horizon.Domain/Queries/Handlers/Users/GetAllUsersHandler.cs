using Horizon.Domain.Lang.PtBr;
using Horizon.Domain.Queries.Inputs.Users;
using Horizon.Domain.Repositories;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using Horizon.Shared.Queries;
using System.Net;

namespace Horizon.Domain.Queries.Handlers.Users
{
    public class GetAllUsersHandler : IQueryHandler<GetAllUsersQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICommandResult> Handle(GetAllUsersQuery query)
        {
            try
            {
                var users = await _userRepository.GetAllAsync(query.Filter, query.page, query.PageSize);

                return new CommandResult(true, string.Empty, (int)HttpStatusCode.OK, users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new CommandResult(false, PtBrMessages.Error, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
