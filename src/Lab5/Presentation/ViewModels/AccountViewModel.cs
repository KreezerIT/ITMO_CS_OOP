using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.ViewModels;

public class AccountViewModel
{
    public string? AccountNumber { get; set; }

    public Money? Balance { get; set; }

    public Collection<TransactionViewModel> Transactions { get; }

    public AccountViewModel()
    {
        Transactions = new Collection<TransactionViewModel>();
    }

    public override string ToString()
    {
        return $"Счет: {AccountNumber}\n" +
               $"Баланс: {Balance?.Amount}' '{Balance?.Currency}\n" +
               $"Количество транзакций: {Transactions.Count}";
    }
}