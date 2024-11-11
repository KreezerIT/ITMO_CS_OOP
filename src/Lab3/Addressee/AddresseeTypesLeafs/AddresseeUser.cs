using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComponents;
using Itmo.ObjectOrientedProgramming.Lab3.Addressee.Logging;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using System.Collections;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeTypesLeafs;

public class AddresseeUser : AddresseeBase
{
    private readonly IRecipient destinationUser;

    public AddresseeUser(IRecipient user, ILogger? logger = null)
        : base(logger)
    {
        destinationUser = user;
    }

    public override void GetMessage(Message message)
    {
        LogReceivedMessage(message);
        destinationUser.RecievedMessages.AddMessage(message);
    }

    public void ReadUserMessages(IEnumerable messages)
    {
        destinationUser.RecievedMessages.ReadMessages(messages);
    }
}