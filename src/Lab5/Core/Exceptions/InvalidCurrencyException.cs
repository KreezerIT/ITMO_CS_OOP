namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

public class InvalidCurrencyException : Exception
{
    public InvalidCurrencyException(string currency)
        : base($"The currency '{currency}' is invalid or not supported.") { }
}