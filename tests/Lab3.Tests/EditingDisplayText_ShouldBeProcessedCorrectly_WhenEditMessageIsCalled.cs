using Itmo.ObjectOrientedProgramming.Lab3;
using Itmo.ObjectOrientedProgramming.Lab3.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators;
using Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators.DecoratorBase;
using Itmo.ObjectOrientedProgramming.Lab3.Displays.Proxy;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Xunit;

namespace Lab3.Tests;

public class EditingDisplayText_ShouldBeProcessedCorrectly_WhenEditMessageIsCalled
{
    [Fact]
    public void TextColorCheck()
    {
        // Arrange
        var realDisplay = new Display();
        var message = new Message("Test Title", "This is a test message");
        var textColorDecorator = new TextColor(null, TextColors.Blue);
        var proxyDisplay = new ProxyDisplayDriver(realDisplay, new List<EditingTextBase> { textColorDecorator });

        proxyDisplay.SetHeldMessage(message);

        // Act
        proxyDisplay.EditMessage();
        string? formattedText = realDisplay.MessageToPrint;

        // Assert
        string expectedText = "\u001b[34mTitle: Test Title\nBody: This is a test message\u001b[0m";
        Assert.Equal(expectedText, formattedText);
    }

    [Fact]
    public void BackgroundTextColorCheck()
    {
        // Arrange
        var realDisplay = new Display();
        var message = new Message("Test Title", "This is a test message");
        var redBackgroundDecorator = new TextBackgroundColor(null, TextColors.Red);
        var proxyDisplay = new ProxyDisplayDriver(realDisplay, new List<EditingTextBase> { redBackgroundDecorator });

        proxyDisplay.SetHeldMessage(message);

        // Act
        proxyDisplay.EditMessage();
        string? formattedText = realDisplay.MessageToPrint;

        // Assert
        string expectedText = "\u001b[41mTitle: Test Title\nBody: This is a test message\u001b[0m";
        Assert.Equal(expectedText, formattedText);
    }
}