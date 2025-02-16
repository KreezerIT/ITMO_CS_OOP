using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileDelete : ICommand
{
    private readonly FileSystem _fileSystem;

    private readonly string _address;

    public FileDelete(string address, FileSystem fileSystem)
    {
        _address = address;
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.FileDelete(_address);
    }
}