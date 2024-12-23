using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;

public class TreeGoToHandler : BaseCommandHandler
{
    public override bool CanHandle(ICommand command)
    {
        return command is TreeGoTo;
    }
}