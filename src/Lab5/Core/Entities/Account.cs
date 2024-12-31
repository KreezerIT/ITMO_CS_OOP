using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;

public class Account
{
    public Guid Id { get; set; }

    public Money Balance { get; set; }

    private readonly List<Transaction> transactionHistory;

    public string AccountNumber { get; set; }

    public string PinCode { get; set; }

    public Account(string accountNumber, string pinCode, CurrencyTypes currency)
    {
        AccountNumber = accountNumber;
        PinCode = pinCode;
        Balance = new Money(0, currency);
        transactionHistory = new List<Transaction>();
    }

    public void AddTransaction(Transaction transaction)
    {
        transactionHistory.Add(transaction);
    }

    public ReadOnlyCollection<Transaction> GetTransactionHistory()
    {
        return transactionHistory.AsReadOnly();
    }
}