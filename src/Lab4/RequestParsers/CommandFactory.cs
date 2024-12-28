using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.Mods;
using System.Text.RegularExpressions;

namespace Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;

public class CommandFactory
{
    private readonly Dictionary<string, Func<string, ICommand>> _commandMappings;

    public CommandFactory(Dictionary<string, Func<string, ICommand>> commandMappings)
    {
        _commandMappings = commandMappings;
    }

    public CommandFactory(FileSystem fileSystem)
    {
        _commandMappings = DefaultCommandDIContainer.CreateContainer(new DefaultModeFactory(), fileSystem);
    }

    public CommandFactory()
    {
        _commandMappings = DefaultCommandDIContainer.CreateContainer(new DefaultModeFactory(), new FileSystem());
    }

    public ICommand CreateCommand(string cmd)
    {
        foreach (KeyValuePair<string, Func<string, ICommand>> mapping in _commandMappings)
        {
            if (Regex.IsMatch(cmd, mapping.Key))
            {
                return mapping.Value(cmd);
            }
        }

        throw new ArgumentException($"Unknown command: {cmd}");
    }
}
