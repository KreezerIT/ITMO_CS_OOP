using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.DTOs;

public class TransactionDTO
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public TransactionType Type { get; set; }

    public Money? UsedMoney { get; set; }

    public TransactionDTO()
    {
        Id = Guid.NewGuid();
    }
}