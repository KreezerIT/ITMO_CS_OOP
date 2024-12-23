using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;

public interface ICommandHandler
{
    void HandleCommand(ICommand command);
}