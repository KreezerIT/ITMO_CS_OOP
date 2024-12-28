using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class TreeList : ICommand
{
    private readonly FileSystem _fileSystem;

    private readonly int _depth;

    public TreeList(int depth, FileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        _depth = depth;
    }

    public void Execute()
    {
        _fileSystem.TreeList(_depth);
    }
}