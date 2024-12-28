namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

public class TransactionFailedException : Exception
{
    public TransactionFailedException(string message)
        : base($"Transaction failed: {message}") { }
}