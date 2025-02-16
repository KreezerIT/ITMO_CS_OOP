using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComponents;
using Itmo.ObjectOrientedProgramming.Lab3.Addressee.Logging;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeTypesLeafs;

public class AddresseeMessenger : AddresseeBase
{
    private readonly IRecipient destinationMessanger;

    public AddresseeMessenger(IRecipient messanger, ILogger? logger = null)
        : base(logger)
    {
        destinationMessanger = messanger;
    }

    public override void GetMessage(Message message)
    {
        LogReceivedMessage(message);
        destinationMessanger.RecievedMessages.AddMessage(message);
    }
}