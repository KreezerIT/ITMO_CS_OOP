using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeTypesLeafs;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class Messanger_ShouldProcessMessage_WhenMessageRecieved
{
    [Fact]
    public void MessangerMessageProcessCheck()
    {
        // Arrange
        var mockMessageKeeper = new Mock<MessageKeeper>();
        var mockMessenger = new Mock<IRecipient>();
        mockMessenger.Setup(u => u.RecievedMessages).Returns(mockMessageKeeper.Object);

        var addresseeMessenger = new AddresseeMessenger(mockMessenger.Object);
        var message = new Message("TestName", "This is a test message");

        // Act
        addresseeMessenger.GetMessage(message);

        // Assert
        mockMessenger.Verify(
            mm => mm.RecievedMessages.AddMessage(message),
            Times.Once);
    }
}