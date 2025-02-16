using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class FileRename_ShouldWorkCorrectly_WhenCommandCalled
{
    [Fact]
    public void FileRename_PathCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string request1 = $"connect {absolutePath} -m local";

        string oldFileName = "File1.txt";
        string filePath = Path.Combine(absolutePath, oldFileName);
        if (!File.Exists(filePath)) File.Create(filePath).Close();

        string newFileName = "File222.txt";
        string newFilePath = Path.Combine(absolutePath, newFileName);
        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        string request2 = $"file rename {filePath} {newFileName}";

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
        Assert.Equal(Path.GetFileName(filePath), oldFileName);
        requestParser.ChangeRequest(request2);
        requestParser.Parse();

        // Assert
        Assert.True(File.Exists(newFilePath));

        File.Delete(newFilePath);
    }

    [Fact]
    public void FileRename_TypeOfCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string oldFileName = "File1.txt";
        string filePath = Path.Combine(absolutePath, oldFileName);
        string newFileName = "File222.txt";

        string request = $"file rename {filePath} {newFileName}";

        var commandFactory = new CommandFactory();

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.IsType<FileRename>(createdCommand);
    }

    [Fact]
    public void FileRename_TypeOfMockCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string oldFileName = "File1.txt";
        string filePath = Path.Combine(absolutePath, oldFileName);
        string newFileName = "File222.txt";

        string request = $"file rename {filePath} {newFileName}";

        var mockFileRenameCommand = new Mock<ICommand>();
        var commandMappings = new Dictionary<string, Func<string, ICommand>>
        {
            { @"^file rename (\S+) (\S+)$", cmd => mockFileRenameCommand.Object },
        };
        var commandFactory = new CommandFactory(commandMappings);

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.Equal(mockFileRenameCommand.Object, createdCommand);
    }
}