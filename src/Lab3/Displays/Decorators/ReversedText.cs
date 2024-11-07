using Crayon;
using Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators.DecoratorBase;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators;

public class ReversedText : EditingTextBase
{
    public ReversedText(EditingTextBase? editingTextBase = null)
        : base(editingTextBase)
    {
    }

    public override string EditText(string text)
    {
        text = Output.Reversed(text);
        return base.EditText(text);
    }
}