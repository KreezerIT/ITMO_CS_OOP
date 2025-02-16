using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;

public abstract class BaseCommandHandler : ICommandHandler
{
    private readonly CommandsHandleHistory _handleHistory = new CommandsHandleHistory();

    private ICommandHandler? _nextHandler;

    protected BaseCommandHandler(ICommandHandler? nextHandler)
    {
        _nextHandler = nextHandler;
    }

    protected BaseCommandHandler() { }

    public ICommandHandler SetNextHandler(ICommandHandler? nextHandler)
    {
        _nextHandler = nextHandler;

        return this;
    }

    public void CommandProcessing(ICommand command)
    {
        command.Execute();
        _handleHistory.AddCommand(command);
    }

    public void GetHandlerHistory()
    {
        _handleHistory.GetHistory();
    }

    public abstract bool CanHandle(ICommand command);

    public virtual void HandleCommand(ICommand command)
    {
        if (CanHandle(command))
        {
            CommandProcessing(command);
        }
        else if (_nextHandler != null)
        {
            _nextHandler.HandleCommand(command);
        }
        else
        {
            throw new ArgumentException($"Command {command.GetType().Name} is not supported");
        }
    }
}