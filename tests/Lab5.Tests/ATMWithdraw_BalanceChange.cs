using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class ATMWithdraw_BalanceChange
{
    [Fact]
    public void Withdraw_ShouldUpdateAccount_WhenBalanceIsSufficient()
    {
        // Arrange
        var mockRepository = new Mock<IAccountRepository>();
        var transactionService = new TransactionService(mockRepository.Object);

        var account = new Account("1234567890", "1234", CurrencyTypes.USD)
        {
            Balance = new Money(100m, CurrencyTypes.USD),
        };

        mockRepository.Setup(repo => repo.Update(It.IsAny<Account>()));

        // Act
        transactionService.Withdraw(account, 50m);

        // Assert
        Assert.Equal(50m, account.Balance.Amount); // Баланс должен уменьшиться
        mockRepository.Verify(
            repo => repo.Update(It.Is<Account>(a =>
            a.Balance.Amount == 50m && a.Balance.Currency == CurrencyTypes.USD)),
            Times.Once);
    }

    [Fact]
    public void Withdraw_ShouldThrowException_WhenBalanceIsInsufficient()
    {
        // Arrange
        var mockRepository = new Mock<IAccountRepository>();
        var transactionService = new TransactionService(mockRepository.Object);

        var account = new Account("1234567890", "1234", CurrencyTypes.USD)
        {
            Balance = new Money(20m, CurrencyTypes.USD),
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => transactionService.Withdraw(account, 50m));
        mockRepository.Verify(repo => repo.Update(It.IsAny<Account>()), Times.Never);
    }
}