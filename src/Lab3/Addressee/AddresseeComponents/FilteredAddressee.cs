using Itmo.ObjectOrientedProgramming.Lab3.Addressee.Logging;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComponents;

public class FilteredAddressee : AddresseeBase
{
    private readonly IComponentAddressee destinationAddressee;

    public ImportanceLevel AcceptableMessageImportanceLevel { get; set; }

    public FilteredAddressee(IComponentAddressee addressee, ImportanceLevel importanceLevel, ILogger? logger = null)
        : base(logger)
    {
        destinationAddressee = addressee;
        AcceptableMessageImportanceLevel = importanceLevel;
    }

    public override void GetMessage(Message message)
    {
        if (message.Importance >= AcceptableMessageImportanceLevel)
        {
            destinationAddressee.GetMessage(message);
            LogReceivedMessage(message);
        }
    }
}