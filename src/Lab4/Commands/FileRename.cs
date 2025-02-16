using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileRename : ICommand
{
    private readonly FileSystem _fileSystem;

    private readonly string _address;

    private readonly string _newFileName;

    public FileRename(string address, string newFileName, FileSystem fileSystem)
    {
        _address = address;
        _newFileName = newFileName;
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.FileRename(_address, _newFileName);
    }
}