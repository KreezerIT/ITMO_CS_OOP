using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Persistence;

public class TransactionRepository : ITransactionRepository
{
    private readonly string _connectionString;

    public TransactionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Save(Transaction transaction)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        const string query = @"
            INSERT INTO Transactions (Id, AccountId, Date, Type, Amount, Currency)
            VALUES (@Id, @AccountId, @Date, @Type, @Amount, @Currency)";

        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", transaction.Id);
        command.Parameters.AddWithValue("@AccountId", transaction.AccountId);
        command.Parameters.AddWithValue("@Date", transaction.Date);
        command.Parameters.AddWithValue("@Type", transaction.Type.ToString());
        command.Parameters.AddWithValue("@Amount", transaction.UsedMoney.Amount);
        command.Parameters.AddWithValue("@Currency", transaction.UsedMoney.Currency.ToString());
        command.ExecuteNonQuery();
    }

    public IReadOnlyCollection<Transaction> GetByAccountId(Guid accountId)
    {
        var transactions = new List<Transaction>();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        const string query = "SELECT * FROM Transactions WHERE AccountId = @AccountId";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@AccountId", accountId);

        using NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            try
            {
                var transaction = new Transaction(
                    (TransactionType)Enum.Parse(
                        typeof(TransactionType),
                        reader["Type"].ToString() ?? throw new InvalidOperationException("Transaction type is null.")),
                    new Money(
                        (decimal)reader["Amount"],
                        (CurrencyTypes)Enum.Parse(typeof(CurrencyTypes), reader["Currency"].ToString() ?? throw new InvalidOperationException("Currency is null."))),
                    (DateTime)reader["Date"])
                {
                    Id = Guid.Parse(reader["Id"].ToString() ??
                                    throw new InvalidOperationException("Transaction ID is null.")),
                    AccountId = Guid.Parse(reader["AccountId"].ToString() ??
                                           throw new InvalidAccountException("Account ID is null.")),
                };

                transactions.Add(transaction);
            }
            catch (Exception ex)
            {
                throw new DataMappingException("Error occurred while mapping transaction data.", ex);
            }
        }

        return transactions;
    }
}