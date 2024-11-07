namespace Itmo.ObjectOrientedProgramming.Lab3.Displays;

public class Display : IDisplay
{
    public string? MessageToPrint { get; private set; }

    public void SetMessage(string text)
    {
        MessageToPrint = text;
    }

    public void PrintMessage()
    {
        Console.WriteLine(MessageToPrint);
    }

    public void ClearOutput()
    {
        Console.Clear();
        MessageToPrint = null;
    }
}