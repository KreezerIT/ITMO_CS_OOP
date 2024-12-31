namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(decimal balance, decimal withdrawalAmount)
        : base($"Insufficient funds. Current balance: {balance}, attempted withdrawal: {withdrawalAmount}.") { }
}