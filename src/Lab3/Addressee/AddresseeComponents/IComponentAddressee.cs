using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComponents;

public interface IComponentAddressee
{
    public Guid Id { get; }

    public void GetMessage(Message message);
}