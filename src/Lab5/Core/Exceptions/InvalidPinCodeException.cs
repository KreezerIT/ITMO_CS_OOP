namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

public class InvalidPinCodeException : Exception
{
    public InvalidPinCodeException()
        : base("The provided PIN code is invalid.") { }
}