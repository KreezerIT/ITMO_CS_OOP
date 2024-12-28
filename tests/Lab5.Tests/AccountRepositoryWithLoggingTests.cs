using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class AccountRepositoryWithLoggingTests
{
    private readonly Mock<IAccountRepository> _mockRepository;
    private readonly Mock<ILogger> _mockLogger;
    private readonly AccountRepositoryWithLogging _repositoryWithLogging;

    public AccountRepositoryWithLoggingTests()
    {
        _mockRepository = new Mock<IAccountRepository>();
        _mockLogger = new Mock<ILogger>();
        _repositoryWithLogging = new AccountRepositoryWithLogging(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public void GetByAccountNumber_LogsAndCallsInnerRepository()
    {
        // Arrange
        const string accountNumber = "12345678901234567890";
        var account = new Account(accountNumber, "1234", CurrencyTypes.USD);
        _mockRepository.Setup(r => r.GetByAccountNumber(accountNumber)).Returns(account);

        // Act
        Account? result = _repositoryWithLogging.GetByAccountNumber(accountNumber);

        // Assert
        Assert.Equal(account, result);
        _mockLogger.Verify(l => l.Log($"Fetching account with AccountNumber: {accountNumber}"), Times.Once);
        _mockRepository.Verify(r => r.GetByAccountNumber(accountNumber), Times.Once);
    }

    [Fact]
    public void Save_LogsAndCallsInnerRepository()
    {
        // Arrange
        var account = new Account("12345678901234567890", "1234", CurrencyTypes.USD);

        // Act
        _repositoryWithLogging.Save(account);

        // Assert
        _mockLogger.Verify(l => l.Log($"Saving account with AccountNumber: {account.AccountNumber}"), Times.Once);
        _mockRepository.Verify(r => r.Save(account), Times.Once);
        _mockLogger.Verify(l => l.Log($"Account with AccountNumber: {account.AccountNumber} saved successfully."), Times.Once);
    }

    [Fact]
    public void Update_LogsAndCallsInnerRepository()
    {
        // Arrange
        var account = new Account("12345678901234567890", "1234", CurrencyTypes.USD);

        // Act
        _repositoryWithLogging.Update(account);

        // Assert
        _mockLogger.Verify(l => l.Log($"Updating account with AccountNumber: {account.AccountNumber}"), Times.Once);
        _mockRepository.Verify(r => r.Update(account), Times.Once);
        _mockLogger.Verify(l => l.Log($"Account with AccountNumber: {account.AccountNumber} updated successfully."), Times.Once);
    }

    [Fact]
    public void Delete_LogsAndCallsInnerRepository()
    {
        // Arrange
        const string accountNumber = "12345678901234567890";

        // Act
        _repositoryWithLogging.Delete(accountNumber);

        // Assert
        _mockLogger.Verify(l => l.Log($"Deleting account with AccountNumber: {accountNumber}"), Times.Once);
        _mockRepository.Verify(r => r.Delete(accountNumber), Times.Once);
        _mockLogger.Verify(l => l.Log($"Account with AccountNumber: {accountNumber} deleted successfully."), Times.Once);
    }

    [Fact]
    public void VerifyAccountPin_LogsAndCallsInnerRepository()
    {
        // Arrange
        const string accountNumber = "12345678901234567890";
        const string pin = "1234";
        _mockRepository.Setup(r => r.VerifyAccountPin(accountNumber, pin)).Returns(true);

        // Act
        bool result = _repositoryWithLogging.VerifyAccountPin(accountNumber, pin);

        // Assert
        Assert.True(result);
        _mockLogger.Verify(l => l.Log($"Verifying PIN for AccountNumber: {accountNumber}"), Times.Once);
        _mockRepository.Verify(r => r.VerifyAccountPin(accountNumber, pin), Times.Once);
        _mockLogger.Verify(l => l.Log($"PIN verification succeeded for AccountNumber: {accountNumber}"), Times.Once);
    }

    [Fact]
    public void VerifyAccountPin_FailsAndLogsFailure()
    {
        // Arrange
        const string accountNumber = "12345678901234567890";
        const string pin = "wrong-pin";
        _mockRepository.Setup(r => r.VerifyAccountPin(accountNumber, pin)).Returns(false);

        // Act
        bool result = _repositoryWithLogging.VerifyAccountPin(accountNumber, pin);

        // Assert
        Assert.False(result);
        _mockLogger.Verify(l => l.Log($"Verifying PIN for AccountNumber: {accountNumber}"), Times.Once);
        _mockRepository.Verify(r => r.VerifyAccountPin(accountNumber, pin), Times.Once);
        _mockLogger.Verify(l => l.Log($"PIN verification failed for AccountNumber: {accountNumber}"), Times.Once);
    }
}