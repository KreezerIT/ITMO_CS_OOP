namespace Itmo.ObjectOrientedProgramming.Lab3.Prototype;

public interface IPrototype<out T>
    where T : IPrototype<T>
{
    T Clone();
}