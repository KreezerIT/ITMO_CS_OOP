using Crayon;
using Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators.DecoratorBase;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators;

public class BoldText : EditingTextBase
{
    public BoldText(EditingTextBase? editingTextBase = null)
        : base(editingTextBase)
    {
    }

    public override string EditText(string text)
    {
        text = Output.Bold(text);
        return base.EditText(text);
    }
}