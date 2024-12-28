using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class TreeGoTo : ICommand
{
    private readonly FileSystem _fileSystem;

    private readonly string _address;

    public TreeGoTo(string address, FileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        _address = address;
    }

    public void Execute()
    {
        _fileSystem.TreeGoTo(_address);
    }
}