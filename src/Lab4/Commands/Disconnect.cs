using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class Disconnect : ICommand
{
    private readonly FileSystem _fileSystem;

    public Disconnect(FileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.Disconnect();
    }
}