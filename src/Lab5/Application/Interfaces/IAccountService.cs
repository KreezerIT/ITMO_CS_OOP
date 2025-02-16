using Itmo.ObjectOrientedProgramming.Lab5.Application.DTOs;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ViewModels;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Interfaces;

public interface IAccountService
{
    void CreateAccount(string accountNumber, string pinCode, string currency);

    string GetBalance(string accountNumber);

    void Deposit(string accountNumber, Money inputedMoney);

    void Withdraw(string accountNumber, decimal moneyToWithdraw);

    AccountDTO GetDTOAccountDetails(string accountNumber);

    IReadOnlyCollection<TransactionViewModel> GetAccountTransactionsByViewModel(string accountNumber);

    bool CheckByAccountNumber(string accountNumber);

    bool VerifyPin(string accountNumber, string pin);
}