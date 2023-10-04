using Horizon.Shared.Commands;

namespace Horizon.Shared.Queries;

public interface IQueryHandler<Query> where Query : IQuery
{
    public Task<ICommandResult> Handle(Query query);
}
