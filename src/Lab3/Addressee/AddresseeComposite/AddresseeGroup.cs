using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComponents;
using Itmo.ObjectOrientedProgramming.Lab3.Addressee.Logging;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComposite;

public class AddresseeGroup : AddresseeBase
{
    private readonly List<IComponentAddressee> componentAddresses;

    public AddresseeGroup(IEnumerable<IComponentAddressee> components, ILogger? logger = null)
        : base(logger)
    {
        componentAddresses = new List<IComponentAddressee>(components);
    }

    public override void GetMessage(Message message)
    {
        LogReceivedMessage(message);
        foreach (IComponentAddressee component in componentAddresses)
        {
            component.GetMessage(message);
        }
    }

    public void AddComponentAddressee(IComponentAddressee componentAddressee)
    {
        componentAddresses.Add(componentAddressee);
    }

    public void RemoveComponentAddressee(IComponentAddressee componentAddressee)
    {
        componentAddresses.Remove(componentAddressee);
    }
}