using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class FileCopy_ShouldWorkCorrectly_WhenCommandCalled
{
    [Fact]
    public void FileCopy_PathCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string request1 = $"connect {absolutePath} -m local";

        string sourcePath = Path.Combine(absolutePath, "File1.txt");
        string destinationPath = Path.Combine(absolutePath, "dir1", "File1.txt");
        string dir1Path = Path.Combine(absolutePath, "dir1");

        if (!Directory.Exists(dir1Path)) Directory.CreateDirectory(dir1Path);
        if (!File.Exists(destinationPath)) File.Create(destinationPath).Close();
        if (File.Exists(sourcePath)) File.Delete(sourcePath);

        string request2 = $"file copy {sourcePath} {destinationPath}";
        File.WriteAllText(sourcePath, "Chpok");

        if (!Directory.Exists(dir1Path)) Directory.CreateDirectory(dir1Path);

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
        string copiedFileContent = File.ReadAllText(destinationPath);

        // Assert
        Assert.True(File.Exists(sourcePath), "File should exist at the source path.");
        Assert.True(File.Exists(destinationPath), "File should be copied to the destination path.");
        Assert.Equal("Chpok", copiedFileContent);

        Console.SetOut(Console.Out);
        File.Delete(destinationPath);
        Directory.Delete(dir1Path);
        File.Delete(sourcePath);
    }

    [Fact]
    public void FileCopy_TypeOfCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string sourcePath = Path.Combine(absolutePath, "File1.txt");
        string destinationPath = Path.Combine(absolutePath, "dir1", "File1.txt");
        string request = $"file copy {sourcePath} {destinationPath}";

        var commandFactory = new CommandFactory();

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.IsType<FileCopy>(createdCommand);
    }

    [Fact]
    public void FileCopy_TypeOfMockCommandCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string sourcePath = Path.Combine(absolutePath, "File1.txt");
        string destinationPath = Path.Combine(absolutePath, "dir1", "File1.txt");
        string request = $"file copy {sourcePath} {destinationPath}";

        var mockFileCopyCommand = new Mock<ICommand>();
        var commandMappings = new Dictionary<string, Func<string, ICommand>>
        {
            { @"^file copy (\S+) (\S+)$", cmd => mockFileCopyCommand.Object },
        };
        var commandFactory = new CommandFactory(commandMappings);

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.Equal(mockFileCopyCommand.Object, createdCommand);
    }
}