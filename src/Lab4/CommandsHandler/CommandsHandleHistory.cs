using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;

public class CommandsHandleHistory
{
    private readonly List<ICommand> _commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void RemoveLastCommand()
    {
        _commands.Remove(_commands.Last());
    }

    public ReadOnlyCollection<ICommand> GetHistory()
    {
        return _commands.AsReadOnly();
    }
}