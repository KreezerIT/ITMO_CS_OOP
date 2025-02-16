using Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Configs;

public class DataBaseConfig
{
    public static string ConnectionString { get; } = "Host=localhost;" +
                                                     "Port=5432;" +
                                                     "Database=PostgreSQL 17;" +
                                                     "Username=postgres;" +
                                                     "Password=POSTitmoGRES12100";

    public static NpgsqlConnection GetConnection()
    {
        var connection = new NpgsqlConnection(ConnectionString);

        try
        {
            connection.Open();
            return connection;
        }
        catch (Exception ex)
        {
            throw new DataMappingException($"DataBase connection error: {ex.Message}");
        }
    }
}