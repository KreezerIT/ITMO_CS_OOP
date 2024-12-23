using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Moq;
using Xunit;
using FileSystem = Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir.FileSystem;

namespace Lab4.Tests;

public class Connect_ShouldWorkCorrectly_WhenCommandCalled
{
    [Fact]
    public void Connect_PathCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string request = $"connect {absolutePath} -m local";

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
        var requestParser = new DefaultRequestParser(request, handlerChain, filesystem);

        // Act
        requestParser.Parse();

        // Assert
        Assert.Equal(@"D:\CS_OOP_4_test", filesystem.ConnectedAbsolutePath);
    }

    [Fact]
    public void Connect_TypeOfCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string request = $"connect {absolutePath} -m local";

        var commandFactory = new CommandFactory();

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.IsType<Connect>(createdCommand);
    }

    [Fact]
    public void Connect_TypeOfMockCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string request = $"connect {absolutePath} -m local";

        var mockConnectCommand = new Mock<ICommand>();
        var commandMappings = new Dictionary<string, Func<string, ICommand>>
        {
            { @"^connect\s", cmd => mockConnectCommand.Object },
        };
        var commandFactory = new CommandFactory(commandMappings);

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.Equal(mockConnectCommand.Object, createdCommand);
    }
}