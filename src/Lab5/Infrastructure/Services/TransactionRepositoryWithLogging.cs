using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;

public class TransactionRepositoryWithLogging : ITransactionRepository
{
    private readonly ITransactionRepository _innerRepository;
    private readonly ILogger _logger;

    public TransactionRepositoryWithLogging(ITransactionRepository innerRepository, ILogger logger)
    {
        _innerRepository = innerRepository;
        _logger = logger;
    }

    public void Save(Transaction transaction)
    {
        _logger.Log($"Saving transaction with Id: {transaction.Id}, AccountId: {transaction.AccountId}");
        _innerRepository.Save(transaction);
        _logger.Log($"Transaction with Id: {transaction.Id} saved successfully.");
    }

    public IReadOnlyCollection<Transaction> GetByAccountId(Guid accountId)
    {
        _logger.Log($"Fetching transactions for AccountId: {accountId}");
        IReadOnlyCollection<Transaction> transactions = _innerRepository.GetByAccountId(accountId);
        _logger.Log($"Fetched {transactions.Count} transactions for AccountId: {accountId}");
        return transactions;
    }
}
