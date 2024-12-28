using Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;

public class Money : IEquatable<Money>
{
    public decimal Amount { get; set; }

    public CurrencyTypes Currency { get; }

    public Money(decimal amount, CurrencyTypes currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.");

        if (string.IsNullOrWhiteSpace(currency.ToString()))
            throw new ArgumentException("Currency cannot be null or empty.");

        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        if (Amount < other.Amount)
            throw new InvalidOperationException("Insufficient funds for this operation.");

        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(decimal factor)
    {
        if (factor < 0)
            throw new ArgumentException("Factor cannot be negative.");

        return new Money(Amount * factor, Currency);
    }

    public bool Equals(Money? other)
    {
        if (other == null) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj)
    {
        return obj is Money money && Equals(money);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public override string ToString()
    {
        return $"{Amount:F2} {Currency}";
    }

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidCurrencyException("Cannot operate on Money objects with different currencies.");
    }

    public static Money operator +(Money a, Money b) => a.Add(b);

    public static Money operator -(Money a, Money b) => a.Subtract(b);

    public static Money operator *(Money a, decimal factor) => a.Multiply(factor);
}