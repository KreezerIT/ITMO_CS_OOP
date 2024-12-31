namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

public class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException()
        : base("You are not authorized to perform this action.") { }
}