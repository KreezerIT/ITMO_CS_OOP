namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.Logging.LoggerTypes;

public class ConsoleLogger : ILogger
{
    public void LogMessage(string message)
    {
        Console.WriteLine(message);
    }
}