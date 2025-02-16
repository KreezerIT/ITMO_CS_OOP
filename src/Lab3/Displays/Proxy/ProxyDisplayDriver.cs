using Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators.DecoratorBase;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Proxy;

public class ProxyDisplayDriver : IDisplay
{
    public Guid Id { get; }

    private readonly Display _realDisplay;

    private List<EditingTextBase> _decorators;

    private Message? _heldMessage;

    private string? _editedText;

    public ProxyDisplayDriver(Display realDisplay, IEnumerable<EditingTextBase> decoratorList)
    {
        Id = Guid.NewGuid();
        _realDisplay = realDisplay;
        _decorators = new List<EditingTextBase>(decoratorList);
    }

    public void SetHeldMessage(Message message)
    {
        _heldMessage = message;
    }

    public void AddDecorators(IEnumerable<EditingTextBase> decorators)
    {
        foreach (EditingTextBase decorator in decorators)
        {
            _decorators.Add(decorator);
        }
    }

    public void SetDecorators(IEnumerable<EditingTextBase> decorators)
    {
        _decorators = new List<EditingTextBase>(decorators);
    }

    public void ClearDecorators()
    {
        _decorators.Clear();
    }

    public void EditMessage()
    {
        if (_heldMessage == null)
        {
            throw new InvalidOperationException("No message to edit.");
        }

        string text = $"Title: {_heldMessage.Title}\nBody: {_heldMessage.Body}";
        foreach (EditingTextBase decorator in _decorators)
        {
            text = decorator.EditText(text);
        }

        _editedText = text;
        _realDisplay.SetMessage(_editedText);
    }

    public void PrintMessage()
    {
        if (_editedText == null)
        {
            throw new InvalidOperationException("No message to display.");
        }

        _realDisplay.PrintMessage();
    }

    public void ClearOutput()
    {
        _realDisplay.ClearOutput();
        _editedText = null;
    }
}