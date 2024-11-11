using Itmo.ObjectOrientedProgramming.Lab3.Addressee.AddresseeTypesLeafs;
using Itmo.ObjectOrientedProgramming.Lab3.Addressee.Logging.LoggerTypes;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class LoggingAddresse_ShouldLogAddresseMessages_WhenMessageRecieved
{
    [Fact]
    public void LoggingAddresseMessagesCheck()
    {
        // Arrange
        var mockLogger = new Mock<MockLogger>();
        var mockMessageKeeper = new Mock<MessageKeeper>();
        var mockUser = new Mock<IRecipient>();
        mockUser.Setup(u => u.RecievedMessages).Returns(mockMessageKeeper.Object);

        var addresseeWithLogging = new AddresseeUser(mockUser.Object, mockLogger.Object);
        var message = new Message("TestName", "This is a test message");

        // Act
        addresseeWithLogging.GetMessage(message);

        // Assert
        mockLogger.Verify(
            logger => logger.LogMessage(It.Is<string>(msg => msg.Contains(message.Title) && msg.Contains(message.Body))),
            Times.Once);

        mockMessageKeeper.Verify(
            mk => mk.AddMessage(message),
            Times.Once);
    }
}