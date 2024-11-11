using Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators.DecoratorBase;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators;

public class TextBackgroundColor : EditingTextBase
{
    private readonly TextColors _color;
    private readonly int? _r;
    private readonly int? _g;
    private readonly int? _b;

    public TextBackgroundColor(EditingTextBase? editingTextBase, TextColors color)
        : base(editingTextBase)
    {
        _color = color;
    }

    public TextBackgroundColor(EditingTextBase editingTextBase, int r, int g, int b)
        : base(editingTextBase)
    {
        _r = r;
        _g = g;
        _b = b;
    }

    public override string EditText(string text)
    {
        text = _r.HasValue && _g.HasValue && _b.HasValue
            ? $"\u001b[48;2;{_r};{_g};{_b}m{text}\u001b[0m"
            : _color switch
            {
                TextColors.Red => $"\u001b[41m{text}\u001b[0m",
                TextColors.Green => $"\u001b[42m{text}\u001b[0m",
                TextColors.Yellow => $"\u001b[43m{text}\u001b[0m",
                TextColors.Blue => $"\u001b[44m{text}\u001b[0m",
                TextColors.Cyan => $"\u001b[46m{text}\u001b[0m",
                TextColors.Magenta => $"\u001b[45m{text}\u001b[0m",
                TextColors.White => $"\u001b[47m{text}\u001b[0m",
                TextColors.Black => $"\u001b[40m{text}\u001b[0m",
                _ => text,
            };

        return base.EditText(text);
    }
}