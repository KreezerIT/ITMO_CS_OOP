using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComponents;
using Itmo.ObjectOrientedProgramming.Lab3.Addressee.Logging;
using Itmo.ObjectOrientedProgramming.Lab3.Displays.Proxy;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeTypesLeafs;

public class AddresseeDisplay : AddresseeBase
{
    private readonly ProxyDisplayDriver destinationDisplay;

    public AddresseeDisplay(ProxyDisplayDriver display, ILogger? logger = null)
        : base(logger)
    {
        destinationDisplay = display;
    }

    public override void GetMessage(Message message)
    {
        LogReceivedMessage(message);
        destinationDisplay.SetHeldMessage(message);
    }
}