using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;

public class AccountRepositoryWithLogging : IAccountRepository
{
    private readonly IAccountRepository _innerRepository;
    private readonly ILogger _logger;

    public AccountRepositoryWithLogging(IAccountRepository innerRepository, ILogger logger)
    {
        _innerRepository = innerRepository;
        _logger = logger;
    }

    public Account? GetByAccountNumber(string accountNumber)
    {
        _logger.Log($"Fetching account with AccountNumber: {accountNumber}");
        return _innerRepository.GetByAccountNumber(accountNumber);
    }

    public void Save(Account account)
    {
        _logger.Log($"Saving account with AccountNumber: {account.AccountNumber}");
        _innerRepository.Save(account);
        _logger.Log($"Account with AccountNumber: {account.AccountNumber} saved successfully.");
    }

    public void Update(Account account)
    {
        _logger.Log($"Updating account with AccountNumber: {account.AccountNumber}");
        _innerRepository.Update(account);
        _logger.Log($"Account with AccountNumber: {account.AccountNumber} updated successfully.");
    }

    public void Delete(string accountNumber)
    {
        _logger.Log($"Deleting account with AccountNumber: {accountNumber}");
        _innerRepository.Delete(accountNumber);
        _logger.Log($"Account with AccountNumber: {accountNumber} deleted successfully.");
    }

    public bool VerifyAccountPin(string accountNumber, string pin)
    {
        _logger.Log($"Verifying PIN for AccountNumber: {accountNumber}");
        bool result = _innerRepository.VerifyAccountPin(accountNumber, pin);
        _logger.Log(result
            ? $"PIN verification succeeded for AccountNumber: {accountNumber}"
            : $"PIN verification failed for AccountNumber: {accountNumber}");

        return result;
    }
}
