using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.DTOs;

public class AccountDTO
{
    public Guid Id { get; set; }

    public Money? Balance { get; set; }

    public IReadOnlyCollection<TransactionDTO> Transactions { get; init; }

    public string? AccountNumber { get; set; }

    public string? PinCode { get; set; }

    public AccountDTO()
    {
        Id = Guid.NewGuid();
        Transactions = new Collection<TransactionDTO>();
    }
}