using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class FileDelete_ShouldWorkCorrectly_WhenCommandCalled
{
    [Fact]
    public void FileDelete_PathCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string request1 = $"connect {absolutePath} -m local";

        string filePath = Path.Combine(absolutePath, "File1.txt");
        if (!File.Exists(filePath)) File.Create(filePath).Close();

        string request2 = $"file delete {filePath}";

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
        Assert.True(File.Exists(filePath), $"File {filePath} not deleted");
        requestParser.ChangeRequest(request2);
        requestParser.Parse();

        // Assert
        Assert.False(File.Exists(filePath), $"File {filePath} was not found");
    }

    [Fact]
    public void FileDelete_TypeOfCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string filePath = Path.Combine(absolutePath, "File1.txt");
        string request = $"file delete {filePath}";

        var commandFactory = new CommandFactory();

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.IsType<FileDelete>(createdCommand);
    }

    [Fact]
    public void FileDelete_TypeOfMockCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string filePath = Path.Combine(absolutePath, "File1.txt");
        string request = $"file delete {filePath}";

        var mockFileDeleteCommand = new Mock<ICommand>();
        var commandMappings = new Dictionary<string, Func<string, ICommand>>
        {
            { @"^file delete (\S+)$", cmd => mockFileDeleteCommand.Object },
        };
        var commandFactory = new CommandFactory(commandMappings);

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.Equal(mockFileDeleteCommand.Object, createdCommand);
    }
}