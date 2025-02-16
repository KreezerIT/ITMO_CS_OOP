using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Configs;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Persistence;

public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AccountRepository()
        {
            _connectionString = AppConfig.ConnectionString;
        }

        public Account? GetByAccountNumber(string accountNumber)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"SELECT * 
                                   FROM Accounts 
                                   WHERE AccountNumber = @AccountNumber";
            using var command = new Npgsql.NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@AccountNumber", accountNumber);

            using Npgsql.NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                try
                {
                    return new Account(
                        reader["AccountNumber"].ToString() ?? throw new InvalidAccountException(accountNumber),
                        reader["PinCode"].ToString() ?? throw new InvalidPinCodeException(),
                        (CurrencyTypes)Enum.Parse(typeof(CurrencyTypes), reader["Currency"].ToString() ?? throw new InvalidOperationException("Missing currency.")))
                    {
                        Id = Guid.Parse(reader["Id"].ToString() ?? throw new InvalidOperationException("Missing ID.")),
                        Balance = new Money(
                            (decimal)reader["Balance"],
                            (CurrencyTypes)Enum.Parse(typeof(CurrencyTypes), reader["Currency"].ToString() ?? throw new InvalidOperationException("Missing currency."))),
                    };
                }
                catch (Exception ex)
                {
                    throw new DataMappingException("Failed to map account data from the database.", ex);
                }
            }

            return null;
        }

        public void Save(Account account)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"INSERT INTO Accounts (Id, AccountNumber, PinCode, Balance, Currency) 
                                   VALUES (@Id, @AccountNumber, @PinCode, @Balance, @Currency)";
            using var command = new Npgsql.NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", account.Id);
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@PinCode", account.PinCode);
            command.Parameters.AddWithValue("@Balance", account.Balance.Amount);
            command.Parameters.AddWithValue("@Currency", account.Balance.Currency.ToString());

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException("Failed to save account to the database.", ex);
            }
        }

        public void Update(Account account)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = "UPDATE Accounts SET Balance = @Balance WHERE AccountNumber = @AccountNumber";
            using var command = new Npgsql.NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@Balance", account.Balance.Amount);

            int affectedRows = command.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                throw new AccountNotFoundException(account.AccountNumber);
            }
        }

        public void Delete(string accountNumber)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = "DELETE FROM Accounts WHERE AccountNumber = @AccountNumber";
            using var command = new Npgsql.NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@AccountNumber", accountNumber);

            int affectedRows = command.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                throw new AccountNotFoundException(accountNumber);
            }
        }

        public bool VerifyAccountPin(string accountNumber, string pin)
        {
            using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = "SELECT PinCode FROM Accounts WHERE AccountNumber = @AccountNumber";
            using var command = new Npgsql.NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@AccountNumber", accountNumber);

            string? storedPin = command.ExecuteScalar()?.ToString();
            if (storedPin == null)
                throw new AccountNotFoundException(accountNumber);

            if (storedPin != pin)
            {
                throw new InvalidPinCodeException();
            }

            return true;
        }
    }