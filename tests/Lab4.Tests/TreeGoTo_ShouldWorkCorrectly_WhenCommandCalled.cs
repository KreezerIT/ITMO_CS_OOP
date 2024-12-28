using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class TreeGoTo_ShouldWorkCorrectly_WhenCommandCalled
{
    [Fact]
    public void TreeGoTo_PathCheck()
    {
        // Arrange
        string absolutePath1 = @"D:\CS_OOP_4_test";
        string request1 = $"connect {absolutePath1} -m local";

        string absolutePath2 = @"D:\CS_OOP_4_test\dir1";
        if (!Directory.Exists(absolutePath2)) Directory.CreateDirectory(absolutePath2);
        string request2 = $"tree goto {absolutePath2}";

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
        Assert.Equal(@"D:\CS_OOP_4_test", filesystem.CurrentAbsolutePath);
        requestParser.ChangeRequest(request2);
        requestParser.Parse();

        // Assert
        Assert.Equal(absolutePath2, filesystem.CurrentAbsolutePath);
        Assert.NotEqual(absolutePath2, filesystem.ConnectedAbsolutePath);

        Directory.Delete(absolutePath2);
    }

    [Fact]
    public void TreeGoTo_TypeOfCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test\dir1";
        string request = $"tree goto {absolutePath}";

        var commandFactory = new CommandFactory();

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.IsType<TreeGoTo>(createdCommand);
    }

    [Fact]
    public void TreeGoTo_TypeOfMockCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test\dir1";
        string request = $"tree goto {absolutePath}";

        var mockTreeGoToCommand = new Mock<ICommand>();
        var commandMappings = new Dictionary<string, Func<string, ICommand>>
        {
            { @"^tree goto (\S+)$", cmd => mockTreeGoToCommand.Object },
        };
        var commandFactory = new CommandFactory(commandMappings);

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.Equal(mockTreeGoToCommand.Object, createdCommand);
    }
}