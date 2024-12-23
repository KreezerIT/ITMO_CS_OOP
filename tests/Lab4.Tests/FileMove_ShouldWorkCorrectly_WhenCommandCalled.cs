using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class FileMove_ShouldWorkCorrectly_WhenCommandCalled
{
    [Fact]
    public void FileMove_PathCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string request1 = $"connect {absolutePath} -m local";

        string sourcePath = Path.Combine(absolutePath, "File1.txt");
        string destinationPath = Path.Combine(absolutePath, "dir1", "File1.txt");

        Directory.CreateDirectory(Path.Combine(absolutePath, "dir1"));
        File.WriteAllText(sourcePath, "Amogus");

        string request2 = $"file move {sourcePath} {destinationPath}";

        string fileContent = "Amogus";
        File.WriteAllText(destinationPath, fileContent);

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

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        requestParser.Parse();
        Assert.Equal(@"D:\CS_OOP_4_test", filesystem.ConnectedAbsolutePath);
        requestParser.ChangeRequest(request2);
        requestParser.Parse();
        string movedFileContent = File.ReadAllText(destinationPath);

        // Assert
        Assert.False(File.Exists(sourcePath), "Should be no file at the source path.");
        Assert.True(File.Exists(destinationPath), "File should be moved to the destination path.");
        Assert.Equal(fileContent, movedFileContent);

        Console.SetOut(Console.Out);
        File.Delete(destinationPath);
        Directory.Delete(Path.Combine(absolutePath, "dir1"));
        File.Delete(sourcePath);
    }

    [Fact]
    public void FileMove_TypeOfCommandCheck()
    {
        // Arrange
        string sourcePath = @"D:\CS_OOP_4_test\File1.txt";
        string destinationPath = @"D:\CS_OOP_4_test\dir1\File1.txt";
        string request = $"file move {sourcePath} {destinationPath}";

        var commandFactory = new CommandFactory();

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.IsType<FileMove>(createdCommand);
    }

    [Fact]
    public void FileMove_TypeOfMockCommandCheck()
    {
        // Arrange
        string sourcePath = @"D:\CS_OOP_4_test\File1.txt";
        string destinationPath = @"D:\CS_OOP_4_test\dir1\File1.txt";
        string request = $"file move {sourcePath} {destinationPath}";

        var mockFileMoveCommand = new Mock<ICommand>();
        var commandMappings = new Dictionary<string, Func<string, ICommand>>
        {
            { @"^file move (\S+) (\S+)$", cmd => mockFileMoveCommand.Object },
        };
        var commandFactory = new CommandFactory(commandMappings);

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.Equal(mockFileMoveCommand.Object, createdCommand);
    }
}