namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators.DecoratorBase;

public abstract class EditingTextBase
{
    private readonly EditingTextBase? editingTextBase;

    protected EditingTextBase(EditingTextBase? editingTextBaseInstance = null)
    {
        editingTextBase = editingTextBaseInstance;
    }

    public virtual string EditText(string text)
    {
        if (editingTextBase != null)
        {
            text = editingTextBase.EditText(text);
        }

        return text;
    }
}