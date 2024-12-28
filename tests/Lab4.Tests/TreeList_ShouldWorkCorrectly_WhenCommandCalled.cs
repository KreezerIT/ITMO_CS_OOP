using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Moq;
using Xunit;

namespace Lab4.Tests;

public class TreeList_ShouldWorkCorrectly_WhenCommandCalled
{
    // [Fact]
    // public void TreeListHandler_ShouldReturnCorrectStructure()
    // {
    //     // Arrange
    //     string tempDirectory = Path.Combine(Path.GetTempPath(), "TreeListTestHandler");
    //     Directory.CreateDirectory(tempDirectory);
    //
    //     // Создание тестовой структуры
    //     string subDir1 = Path.Combine(tempDirectory, "SubDir1");
    //     string subDir2 = Path.Combine(tempDirectory, "SubDir2");
    //     Directory.CreateDirectory(subDir1);
    //     Directory.CreateDirectory(subDir2);
    //
    //     string file1 = Path.Combine(tempDirectory, "File1.txt");
    //     string file2 = Path.Combine(subDir1, "File2.txt");
    //     File.WriteAllText(file1, "Test File 1");
    //     File.WriteAllText(file2, "Test File 2");
    //
    //     var fileSystem = new FileSystem();
    //     fileSystem.CurrentAbsolutePath = tempDirectory;
    //
    //     var treeListHandler = new TreeListHandler();
    //     Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.ICommandHandler handlerChain = new ConnectHandler()
    //         .SetNextHandler(new DisconnectHandler()
    //             .SetNextHandler(treeListHandler));
    //
    //     using var stringWriter = new StringWriter();
    //     Console.SetOut(stringWriter);
    //
    //     var treeListCommand = new TreeList(2, fileSystem);
    //     handlerChain.HandleCommand(treeListCommand);
    //
    //     // Act
    //     string actualOutput = NormalizeLineEndings(stringWriter.ToString());
    //
    //     // Assert
    //     string expectedOutput = NormalizeLineEndings(
    //         "┣━ SubDir1\n" +
    //         "┃ ┗━ File2.txt\n" +
    //         "┣━ SubDir2\n" +
    //         "┗━ File1.txt\n");
    //
    //     Assert.Equal(expectedOutput, actualOutput);
    //
    //     // Удаление временной директории
    //     Directory.Delete(tempDirectory, true);
    // }

// Normalize line endings
    // public string NormalizeLineEndings(string input)
    // {
    //     return input
    //         .Replace("\r\n", "\n", StringComparison.Ordinal)
    //         .Replace("\r", "\n", StringComparison.Ordinal);
    // }
    [Fact]
    public void TreeList_TypeOfCommandCheck()
    {
        // Arrange
        int depth = 2;
        string request = $"tree list -d {depth}";

        var commandFactory = new CommandFactory();

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.IsType<TreeList>(createdCommand);
    }

    [Fact]
    public void TreeList_TypeOfMockCommandCheck()
    {
        // Arrange
        int depth = 2;
        string request = $"tree list -d {depth}";

        var mockTreeListCommand = new Mock<ICommand>();
        var commandMappings = new Dictionary<string, Func<string, ICommand>>
        {
            { @"^tree list -d (\d+)$", cmd => mockTreeListCommand.Object },
        };
        var commandFactory = new CommandFactory(commandMappings);

        // Act
        ICommand createdCommand = commandFactory.CreateCommand(request);

        // Assert
        Assert.Equal(mockTreeListCommand.Object, createdCommand);
    }
}