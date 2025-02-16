namespace Itmo.ObjectOrientedProgramming.Lab4.Mods;

public interface IModeFactory
{
    IMode CreateMode(string modeName);
}