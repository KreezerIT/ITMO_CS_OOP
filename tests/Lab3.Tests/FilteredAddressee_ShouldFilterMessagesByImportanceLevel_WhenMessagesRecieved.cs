using Itmo.ObjectOrientedProgramming.Lab3;
using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComponents;
using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeComposite;
using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeTypesLeafs;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class FilteredAddressee_ShouldFilterMessagesByImportanceLevel_WhenMessagesRecieved
{
    [Fact]
    public void FilteringAddresseeIncomingMessages()
    {
        // Arrange
        var mockAddressee = new Mock<IComponentAddressee>();
        var filteredAddressee = new FilteredAddressee(mockAddressee.Object, ImportanceLevel.High);
        var message = new Message("MessageStatus_ShouldBeUpdated_WhenUserRead Title", "This is a normal-importance message", ImportanceLevel.Normal);

        // Act
        filteredAddressee.GetMessage(message);

        // Assert
        mockAddressee.Verify(
            addressee => addressee.GetMessage(It.IsAny<Message>()),
            Times.Never);
    }

    [Fact]
    public void FilteringMultipleAddresseeIncomingMessages()
    {
        // Arrange
        var messageKeeper = new MessageKeeper();
        User user = new("TestName", messageKeeper);
        var message = new Message("MessageStatus_ShouldBeUpdated_WhenUserRead Title", "This is a normal-importance message", ImportanceLevel.Normal);

        var addresseeUser1 = new AddresseeUser(user);
        var addresseeUser2 = new AddresseeUser(user);
        var filteredAddresseeUser2 = new FilteredAddressee(addresseeUser2, ImportanceLevel.High);

        var addresseGroup = new AddresseeGroup(new List<IComponentAddressee> { addresseeUser1, filteredAddresseeUser2 });

        // Act
        addresseGroup.GetMessage(message);

        // Assert
        Assert.Single(user.GetMessages());
        Assert.Equal(message, user.GetMessages().First().Key);
    }
}