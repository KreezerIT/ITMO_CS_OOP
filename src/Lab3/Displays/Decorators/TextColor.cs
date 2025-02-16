using Crayon;
using Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators.DecoratorBase;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators;

public class TextColor : EditingTextBase
{
    private readonly TextColors _color;
    private readonly int? _r;
    private readonly int? _g;
    private readonly int? _b;

    public TextColor(EditingTextBase? editingTextBase, TextColors color)
        : base(editingTextBase)
    {
        _color = color;
    }

    public TextColor(EditingTextBase editingTextBase, int r, int g, int b)
        : base(editingTextBase)
    {
        _r = r;
        _g = g;
        _b = b;
    }

    public override string EditText(string text)
    {
        text = _r.HasValue && _g.HasValue && _b.HasValue
            ? $"\u001b[38;2;{_r};{_g};{_b}m{text}\u001b[0m"
            : _color switch
            {
                TextColors.Red => Output.Red(text),
                TextColors.Green => Output.Green(text),
                TextColors.Yellow => Output.Yellow(text),
                TextColors.Blue => Output.Blue(text),
                TextColors.Cyan => Output.Cyan(text),
                TextColors.Magenta => Output.Magenta(text),
                TextColors.White => Output.White(text),
                TextColors.Black => Output.Black(text),
                _ => text,
            };

        return base.EditText(text);
    }
}