﻿using Crayon;
using Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators.DecoratorBase;

namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Decorators;

public class DimText : EditingTextBase
{
    public DimText(EditingTextBase? editingTextBase = null)
        : base(editingTextBase)
    {
    }

    public override string EditText(string text)
    {
        text = Output.Dim(text);
        return base.EditText(text);
    }
}