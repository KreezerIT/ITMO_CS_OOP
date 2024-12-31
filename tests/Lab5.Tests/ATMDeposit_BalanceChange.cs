using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class ATMDeposit_BalanceChange
{
    [Fact]
    public void Deposit_ShouldUpdateAccount_WhenCalled()
    {
        // Arrange
        var mockRepository = new Mock<IAccountRepository>();
        var transactionService = new TransactionService(mockRepository.Object);

        var account = new Account("1234567890", "1234", CurrencyTypes.USD)
        {
            Balance = new Money(100m, CurrencyTypes.USD),
        };

        mockRepository.Setup(repo => repo.Update(It.IsAny<Account>()));

        var depositAmount = new Money(50m, CurrencyTypes.USD);

        // Act
        transactionService.Deposit(account, depositAmount);

        // Assert
        Assert.Equal(150m, account.Balance.Amount);
        mockRepository.Verify(
            repo => repo.Update(It.Is<Account>(a =>
            a.Balance.Amount == 150m && a.Balance.Currency == CurrencyTypes.USD)),
            Times.Once);
    }
}