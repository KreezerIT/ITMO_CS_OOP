using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public interface IRecipient
{
    Guid Id { get; }

    string Name { get; set; }

    MessageKeeper RecievedMessages { get; }

    Dictionary<Message, MessageStatus> GetMessages();
}