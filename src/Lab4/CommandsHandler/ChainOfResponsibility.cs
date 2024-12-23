using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;

public class ChainOfResponsibility : ICommandHandler
{
    public ICommandHandler? RootHandler { get; }

    public ChainOfResponsibility(ICommandHandler? rootHandler)
    {
        RootHandler = rootHandler;
    }

    public void HandleCommand(ICommand command)
    {
        command.Execute();
    }
}