namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

public class InvalidInputException : Exception
{
    public InvalidInputException(string input, string reason)
        : base($"The input '{input}' is invalid: {reason}.") { }
}