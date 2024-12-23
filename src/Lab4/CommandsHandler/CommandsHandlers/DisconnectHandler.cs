using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;

public class DisconnectHandler : BaseCommandHandler
{
    public DisconnectHandler(ICommandHandler? nextHandler = null) : base(nextHandler) { }

    public override bool CanHandle(ICommand command)
    {
        return command is Disconnect;
    }
}