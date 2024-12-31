namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

public class DatabaseOperationException : Exception
{
    public DatabaseOperationException(string message)
        : base(message)
    {
    }

    public DatabaseOperationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}