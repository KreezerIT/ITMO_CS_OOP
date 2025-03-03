﻿using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.Mods;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileShow : ICommand
{
    private readonly FileSystem _fileSystem;

    private readonly string _address;

    private readonly IMode _mode;

    public FileShow(string address, IMode mode, FileSystem fileSystem)
    {
        _address = address;
        _mode = mode;
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.FileShow(_address, _mode);
    }
}