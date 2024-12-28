using Itmo.ObjectOrientedProgramming.Lab4.Mods.ModeTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Mods;

public class DefaultModeFactory : IModeFactory
{
    public IMode CreateMode(string modeName)
    {
        return modeName switch
        {
            "local" => new LocalMode(),
            _ => throw new ArgumentException($"Unknown mode: {modeName}"),
        };
    }
}