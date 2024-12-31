using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Configs;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public FileLogger()
    {
        _filePath = AppConfig.LogFilePath;
    }

    public void Log(string message)
    {
        File.AppendAllText(_filePath, $"[FileLogger] {message}{Environment.NewLine}");
    }
}