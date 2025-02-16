using System.Collections;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class MessageKeeper
{
    private Dictionary<Message, MessageStatus> Messages { get; }

    public Dictionary<Message, MessageStatus> GetMessages()
    {
        return Messages;
    }

    public virtual void AddMessage(Message message)
    {
        Messages.Add(message, MessageStatus.ItIsNotRead);
    }

    public void ReadMessages(IEnumerable messages)
    {
        foreach (Message message in messages)
        {
            _ = Messages[message] == MessageStatus.ItIsRead
                ? throw new InvalidOperationException($"Message with Title: '{message.Title}' & Body: '{message.Body}' has already been read.")
                : Messages[message] = MessageStatus.ItIsRead;
        }
    }

    public MessageKeeper()
    {
        Messages = new Dictionary<Message, MessageStatus>();
    }
}