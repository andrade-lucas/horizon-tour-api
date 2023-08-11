namespace Horizon.Shared.Commands;

public interface ICommandHandler<Command> where Command : ICommand
{
    public ICommandResult Handle(Command command);
}
