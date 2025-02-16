namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.Logging.LoggerTypes;

public class MockLogger : ILogger
{
    public int CallCount { get; private set; }

    private List<string> LogMessages { get; } = new List<string>();

    public virtual void LogMessage(string message)
    {
        CallCount++;
        LogMessages.Add(message);
    }

    public void Clear()
    {
        CallCount = 0;
        LogMessages.Clear();
    }
}