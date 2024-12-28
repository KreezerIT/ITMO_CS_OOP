using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class TransactionRepositoryWithLoggingTests
{
    private readonly Mock<ITransactionRepository> _mockRepository;
    private readonly Mock<ILogger> _mockLogger;
    private readonly TransactionRepositoryWithLogging _repositoryWithLogging;

    public TransactionRepositoryWithLoggingTests()
    {
        _mockRepository = new Mock<ITransactionRepository>();
        _mockLogger = new Mock<ILogger>();
        _repositoryWithLogging = new TransactionRepositoryWithLogging(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public void Save_LogsAndCallsInnerRepository()
    {
        // Arrange
        var transaction = new Transaction(
            TransactionType.AccountReplenishment,
            new Money(100m, CurrencyTypes.USD),
            DateTime.UtcNow)
        {
            Id = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
        };

        // Act
        _repositoryWithLogging.Save(transaction);

        // Assert
        _mockLogger.Verify(l => l.Log($"Saving transaction with Id: {transaction.Id}, AccountId: {transaction.AccountId}"), Times.Once);
        _mockRepository.Verify(r => r.Save(transaction), Times.Once);
        _mockLogger.Verify(l => l.Log($"Transaction with Id: {transaction.Id} saved successfully."), Times.Once);
    }

    [Fact]
    public void GetByAccountId_LogsAndCallsInnerRepository()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var transactions = new List<Transaction>
        {
            new Transaction(TransactionType.AccountReplenishment, new Money(100m, CurrencyTypes.USD), DateTime.UtcNow)
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
            },
            new Transaction(TransactionType.WithdrawalFromAccount, new Money(50m, CurrencyTypes.USD), DateTime.UtcNow)
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
            },
        };

        _mockRepository.Setup(r => r.GetByAccountId(accountId)).Returns(transactions);

        // Act
        IReadOnlyCollection<Transaction> result = _repositoryWithLogging.GetByAccountId(accountId);

        // Assert
        Assert.Equal(transactions, result);
        _mockLogger.Verify(l => l.Log($"Fetching transactions for AccountId: {accountId}"), Times.Once);
        _mockRepository.Verify(r => r.GetByAccountId(accountId), Times.Once);
        _mockLogger.Verify(l => l.Log($"Fetched {transactions.Count} transactions for AccountId: {accountId}"), Times.Once);
    }
}