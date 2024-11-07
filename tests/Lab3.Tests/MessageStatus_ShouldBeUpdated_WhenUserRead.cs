using Itmo.ObjectOrientedProgramming.Lab3;
using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeTypesLeafs;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Xunit;

namespace Lab3.Tests;

public class MessageStatus_ShouldBeUpdated_WhenUserRead
{
    [Fact]
    public void UserMessagesRemainInUnreadStatus()
    {
        // Arrange
        var messageKeeper = new MessageKeeper();
        User user = new("TestName", messageKeeper);
        var addresseUser = new AddresseeUser(user);
        var message = new Message("MessageStatus_ShouldBeUpdated_WhenUserRead", "Message for tests");

        // Act
        addresseUser.GetMessage(message);

        // Assert
        Assert.Equal(MessageStatus.ItIsNotRead, user.GetMessages()[message]);
    }

    [Fact]
    public void UserMessagesUpdateOnReadStatus()
    {
        // Arrange
        var messageKeeper = new MessageKeeper();
        User user = new("TestName", messageKeeper);
        var addresseUser = new AddresseeUser(user);
        var message = new Message("MessageStatus_ShouldBeUpdated_WhenUserRead", "Message for tests");

        // Act
        addresseUser.GetMessage(message);
        addresseUser.ReadUserMessages(new List<Message> { message });

        // Assert
        Assert.Equal(MessageStatus.ItIsRead, user.GetMessages()[message]);
    }

    [Fact]
    public void UserMessagesAreNotMarkedAsReadTwice()
    {
        // Arrange
        var messageKeeper = new MessageKeeper();
        User user = new("TestName", messageKeeper);
        var addresseUser = new AddresseeUser(user);
        var message = new Message("MessageStatus_ShouldBeUpdated_WhenUserRead", "Message for tests");

        // Act
        addresseUser.GetMessage(message);
        addresseUser.ReadUserMessages(new List<Message> { message });

        // Assert
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => addresseUser.ReadUserMessages(new List<Message> { message }));
        Assert.Equal($"Message with Title: '{message.Title}' & Body: '{message.Body}' has already been read.", exception.Message);
    }
}