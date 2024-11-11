using Crayon;
using Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators.DecoratorBase;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators;

public class UnderlineText : EditingTextBase
{
    public UnderlineText(EditingTextBase? editingTextBase = null)
        : base(editingTextBase)
    {
    }

    public override string EditText(string text)
    {
        text = Output.Underline(text);
        return base.EditText(text);
    }
}