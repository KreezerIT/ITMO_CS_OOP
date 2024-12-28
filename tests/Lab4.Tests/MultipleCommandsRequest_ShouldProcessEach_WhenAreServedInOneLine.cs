using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;
using Xunit;

namespace Lab4.Tests;

public class MultipleCommandsRequest_ShouldProcessEach_WhenAreServedInOneLine
{
    [Fact]
    public void Connect_Disconnect_PathCheck()
    {
        // Arrange
        string absolutePath = @"D:\CS_OOP_4_test";
        string request = $"connect {absolutePath} -m local disconnect";

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
        Assert.Equal(string.Empty, filesystem.ConnectedAbsolutePath);
    }

    [Fact]
    public void Connect_TreeGoTo_PathCheck()
    {
        // Arrange
        string absolutePath1 = @"D:\CS_OOP_4_test";
        string absolutePath2 = @"D:\CS_OOP_4_test\dir1";
        string request = $"connect {absolutePath1} -m local tree goto {absolutePath2}";

        if (!Directory.Exists(absolutePath2)) Directory.CreateDirectory(absolutePath2);

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
        Assert.Equal(absolutePath2, filesystem.CurrentAbsolutePath);

        Directory.Delete(absolutePath2);
    }

    [Fact]
    public void Connect_TreeGoTo_FileDelete_PathCheck()
    {
        // Arrange
        string absolutePath1 = @"D:\CS_OOP_4_test";
        string absolutePath2 = @"D:\CS_OOP_4_test\dir1";
        string absolutePath3 = @"D:\CS_OOP_4_test\dir1\File1.txt";

        string request = $"connect {absolutePath1} -m local tree goto {absolutePath2} file delete {absolutePath3}";

        if (!Directory.Exists(absolutePath2)) Directory.CreateDirectory(absolutePath2);
        if (!File.Exists(absolutePath3)) File.Create(absolutePath3);

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
        Assert.Equal(absolutePath2, filesystem.CurrentAbsolutePath);
        Assert.False(Path.Exists(absolutePath3));

        Directory.Delete(absolutePath2);
    }
}