using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using System.Collections;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class User : IRecipient
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public MessageKeeper RecievedMessages { get; set; }

    public Dictionary<Message, MessageStatus> GetMessages()
    {
        return RecievedMessages.GetMessages();
    }

    public void ReadMessages(IEnumerable messages)
    {
        RecievedMessages.ReadMessages(messages);
    }

    public User(string name, MessageKeeper messages)
    {
        Name = name;
        RecievedMessages = messages;
        Id = Guid.NewGuid();
    }
}