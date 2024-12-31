namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

public class DataMappingException : Exception
{
    public DataMappingException(string message)
        : base(message)
    {
    }

    public DataMappingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}