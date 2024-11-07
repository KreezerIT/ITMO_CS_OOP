using Itmo.ObjectOrientedProgramming.Lab3.Addressee.Logging;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComponents;

public abstract class AddresseeBase : IComponentAddressee
{
    public Guid Id { get; }

    private readonly ILogger? addresseeLogger;

    protected AddresseeBase(ILogger? logger = null)
    {
        Id = Guid.NewGuid();
        addresseeLogger = logger;
    }

    public abstract void GetMessage(Message message);

    protected void LogReceivedMessage(Message message)
    {
        addresseeLogger?.LogMessage($"Message with Title: {message.Title} & Body: {message.Body} received to recipient with ID: {Id}");
    }
}