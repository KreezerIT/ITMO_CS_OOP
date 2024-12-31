using Itmo.ObjectOrientedProgramming.Lab5.Application.DTOs;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ViewModels;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Interfaces;

public interface ITransactionService
{
    void Deposit(Account account, Money amount);

    void Withdraw(Account account, decimal amount);

    IReadOnlyCollection<TransactionDTO> GetDTOTransactionHistory(Account account);

    IReadOnlyCollection<TransactionViewModel> GetTransactionHistoryByViewModel(Account account);
}