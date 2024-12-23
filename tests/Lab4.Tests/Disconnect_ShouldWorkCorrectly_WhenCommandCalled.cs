using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class Disconnect_ShouldWorkCorrectly_WhenCommandCalled
{
    [Fact]
    public void Disconnect_PathCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string request1 = $"connect {absolutePath} -m local";
        string request2 = "disconnect";

        var handlerChain = new ChainOfResponsibility(
            new ConnectHandler()
                .SetNextHandler(new DisconnectHandler()
                    .SetNextHandler(new TreeGoToHandler()
                        .SetNextHandler(new TreeListHandler()
                            .SetNextHandler(new FileShowHandler()
                                .SetNextHandler(new FileMoveHandler()
                                    .SetNextHandler(new FileCopyHandler()
                                        .SetNextHandler(new FileDeleteHandler()
                                            .SetNextHandler(new FileRenameHandler())))))))));

        var filesystem = new FileSystem();
        var requestParser = new DefaultRequestParser(request1, handlerChain, filesystem);

        // Act
        requestParser.Parse();
        Assert.Equal(@"D:\CS_OOP_4_test", filesystem.ConnectedAbsolutePath);
        requestParser.ChangeRequest(request2);
        requestParser.Parse();

        // Assert
        Assert.Equal(string.Empty, filesystem.ConnectedAbsolutePath);
    }

    [Fact]
    public void Disconnect_TypeOfCommandCheck()
    {
        // Arrange
        string request = "disconnect";

        var commandFactory = new CommandFactory();

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.IsType<Disconnect>(createdCommand);
    }

    [Fact]
    public void Disconnect_TypeOfMockCommandCheck()
    {
        // Arrange
        string request = "disconnect";

        var mockDisconnectCommand = new Mock<ICommand>();
        var commandMappings = new Dictionary<string, Func<string, ICommand>>
        {
            { @"^disconnect$", cmd => mockDisconnectCommand.Object },
        };
        var commandFactory = new CommandFactory(commandMappings);

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.Equal(mockDisconnectCommand.Object, createdCommand);
    }
}