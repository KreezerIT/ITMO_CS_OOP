using Itmo.ObjectOrientedProgramming.Lab5.Application.DTOs;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ViewModels;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;
    private readonly ITransactionService _transactionService;

    public AccountService(IAccountRepository repository, ITransactionService transactionService)
    {
        _repository = repository;
        _transactionService = transactionService;
    }

    public void CreateAccount(string accountNumber, string pinCode, string currency)
    {
        if (!Enum.TryParse<CurrencyTypes>(currency, out CurrencyTypes currencyType))
        {
            throw new ArgumentException($"Invalid currency type: {currency}. Valid values are: {string.Join(", ", Enum.GetNames(typeof(CurrencyTypes)))}");
        }

        var account = new Account(accountNumber, pinCode, Enum.Parse<CurrencyTypes>(currency));
        _repository.Save(account);
    }

    public string GetBalance(string accountNumber)
    {
        Account? account = _repository.GetByAccountNumber(accountNumber);
        if (account == null)
            throw new AccountNotFoundException("Invalid account number.");

        return $"{account.Balance.Amount}' '{account.Balance.Currency}";
    }

    public void Deposit(string accountNumber, Money inputedMoney)
    {
        Account? account = _repository.GetByAccountNumber(accountNumber);
        if (account == null)
            throw new InvalidAccountException("Invalid account number.");

        _transactionService.Deposit(account, inputedMoney);
    }

    public void Withdraw(string accountNumber, decimal moneyToWithdraw)
    {
        Account? account = _repository.GetByAccountNumber(accountNumber);
        if (account == null)
            throw new InvalidAccountException("Invalid account number.");

        _transactionService.Withdraw(account, moneyToWithdraw);
    }

    public AccountDTO GetDTOAccountDetails(string accountNumber)
    {
        Account? account = _repository.GetByAccountNumber(accountNumber);
        if (account == null)
            throw new InvalidAccountException("Invalid account number.");

        return new AccountDTO
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Transactions = _transactionService.GetDTOTransactionHistory(account),
        };
    }

    public IReadOnlyCollection<TransactionViewModel> GetAccountTransactionsByViewModel(string accountNumber)
    {
        Account? account = _repository.GetByAccountNumber(accountNumber);
        if (account == null)
            throw new InvalidAccountException("Invalid account number.");

        return _transactionService.GetTransactionHistoryByViewModel(account);
    }

    public bool CheckByAccountNumber(string accountNumber)
    {
        ArgumentException.ThrowIfNullOrEmpty(accountNumber);

        Account? account = _repository.GetByAccountNumber(accountNumber);
        return account != null;
    }

    public bool VerifyPin(string accountNumber, string pin)
    {
        ArgumentException.ThrowIfNullOrEmpty(accountNumber);
        ArgumentException.ThrowIfNullOrEmpty(pin);

        return _repository.VerifyAccountPin(accountNumber, pin);
    }
}