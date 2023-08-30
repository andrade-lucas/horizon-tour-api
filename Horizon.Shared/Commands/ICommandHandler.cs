namespace Horizon.Shared.Commands;

public interface ICommandHandler<Command> where Command : ICommand
{
    public Task<ICommandResult> Handle(Command command);
}
