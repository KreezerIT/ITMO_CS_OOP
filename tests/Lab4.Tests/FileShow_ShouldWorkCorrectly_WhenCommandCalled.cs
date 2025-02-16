using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class FileShow_ShouldWorkCorrectly_WhenCommandCalled
{
    [Fact]
    public void FileShow_PathCheck()
    {
        // Arrange
        string absolutePath1 = @"D:\CS_OOP_4_test";
        string request1 = $"connect {absolutePath1} -m local";

        string absolutePath2 = @"D:\CS_OOP_4_test\File1.txt";
        string request2 = $"file show {absolutePath2} -m local";

        string testFilePath = Path.Combine(absolutePath1, "File1.txt");
        Directory.CreateDirectory(absolutePath1);
        File.WriteAllText(testFilePath, "Aboba");

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

        // Assert
        string output = stringWriter.ToString();
        Assert.Contains("File 'File1.txt' content:", output, StringComparison.Ordinal);
        Assert.Contains("Aboba", output, StringComparison.Ordinal);

        Console.SetOut(Console.Out);
        File.Delete(testFilePath);
    }

    [Fact]
    public void FileShow_TypeOfCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test\File1.txt";
        string request = $"file show {absolutePath} -m local";

        var commandFactory = new CommandFactory();

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.IsType<FileShow>(createdCommand);
    }

    [Fact]
    public void FileShow_TypeOfMockCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test\File1.txt";
        string request = $"file show {absolutePath} -m local";

        var mockFileShowCommand = new Mock<ICommand>();
        var commandMappings = new Dictionary<string, Func<string, ICommand>>
        {
            { @"^file show (\S+) -m (\S+)$", cmd => mockFileShowCommand.Object },
        };
        var commandFactory = new CommandFactory(commandMappings);

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.Equal(mockFileShowCommand.Object, createdCommand);
    }
}