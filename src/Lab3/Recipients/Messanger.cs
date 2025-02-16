using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class Messanger : IRecipient
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public MessageKeeper RecievedMessages { get; set; }

    public Dictionary<Message, MessageStatus> GetMessages()
    {
        return RecievedMessages.GetMessages();
    }

    public void PrintMessage(Message message)
    {
        Console.WriteLine($"Messanger {Name}: {message}");
    }

    public Messanger(string name, MessageKeeper messages)
    {
        Name = name;
        RecievedMessages = messages;
        Id = Guid.NewGuid();
    }
}