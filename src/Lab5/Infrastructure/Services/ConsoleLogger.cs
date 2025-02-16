using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[ConsoleLogger] {message}");
    }
}
