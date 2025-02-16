using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComponents;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeTypesLeafs;

public class Topic : IComponentAddressee
{
    private readonly List<IComponentAddressee> topicAddressees;

    public Guid Id { get; set; }

    public string Name { get; set; }

    public void GetMessage(Message message)
    {
        foreach (IComponentAddressee addressee in topicAddressees)
        {
            addressee.GetMessage(message);
        }
    }

    public Topic(string name, IEnumerable<IComponentAddressee> addressees)
    {
        Id = Guid.NewGuid();
        Name = name;
        topicAddressees = new List<IComponentAddressee>(addressees);
    }
}