using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileMove : ICommand
{
    private readonly FileSystem _fileSystem;

    private readonly string _sourceAddress;

    private readonly string _destinationAddress;

    public FileMove(string sourceAddress, string destinationAddress, FileSystem fileSystem)
    {
        _sourceAddress = sourceAddress;
        _destinationAddress = destinationAddress;
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.FileMove(_sourceAddress, _destinationAddress);
    }
}