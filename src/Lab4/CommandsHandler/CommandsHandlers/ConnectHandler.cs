using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;

public class ConnectHandler : BaseCommandHandler
{
    public ConnectHandler(ICommandHandler? commandHandler = null) { }

    public override bool CanHandle(ICommand command)
    {
        return command is Connect;
    }
}