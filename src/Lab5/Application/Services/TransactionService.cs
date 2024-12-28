using Itmo.ObjectOrientedProgramming.Lab5.Application.DTOs;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ViewModels;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IAccountRepository _accountRepository;

    public TransactionService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void Deposit(Account account, Money amount)
    {
        if (account.Balance.Currency != amount.Currency) throw new InvalidOperationException();

        ArgumentNullException.ThrowIfNull(account);
        account.Balance += amount;
        account.AddTransaction(new Transaction(TransactionType.AccountReplenishment, amount, DateTime.Now));
        _accountRepository.Update(account);
    }

    public void Withdraw(Account account, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (account.Balance.Amount < amount)
            throw new InvalidOperationException("Insufficient funds.");

        account.Balance.Amount -= amount;
        account.AddTransaction(new Transaction(TransactionType.WithdrawalFromAccount, new Money(amount, account.Balance.Currency), DateTime.Now));
        _accountRepository.Update(account);
    }

    public IReadOnlyCollection<TransactionDTO> GetDTOTransactionHistory(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        return new Collection<TransactionDTO>(
            account.GetTransactionHistory().Select(t => new TransactionDTO
            {
                Id = t.Id,
                Date = t.Date,
                Type = t.Type,
                UsedMoney = t.UsedMoney,
            }).ToList());
    }

    public IReadOnlyCollection<TransactionViewModel> GetTransactionHistoryByViewModel(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        IReadOnlyCollection<TransactionDTO> transactionDTOs = GetDTOTransactionHistory(account);

        var transactionViewModels = transactionDTOs
            .Select(TransactionViewModel.FromDTO)
            .ToList();

        return transactionViewModels;
    }
}