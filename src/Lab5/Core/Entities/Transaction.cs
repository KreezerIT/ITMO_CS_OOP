using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;

public class Transaction
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public DateTime Date { get; set; }

    public TransactionType Type { get; set; }

    public Money UsedMoney { get; set; }

    public Transaction(TransactionType type, Money usedMoney, DateTime date)
    {
        Id = Guid.NewGuid();
        Type = type;
        UsedMoney = usedMoney;
        Date = date;
    }
}