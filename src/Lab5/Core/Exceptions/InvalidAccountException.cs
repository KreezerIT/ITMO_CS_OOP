namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

public class InvalidAccountException : Exception
{
    public InvalidAccountException(string accountNumber)
        : base($"The account with number '{accountNumber}' is invalid or does not exist.") { }
}