namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Configs;

public static class AppConfig
{
    private static readonly Dictionary<string, string> CommandLineArgs = new();

    public static void Initialize(string[] args)
    {
        foreach (string arg in args)
        {
            string[] split = arg.Split('=', 2);
            if (split.Length == 2)
            {
                CommandLineArgs[split[0]] = split[1];
            }
        }
    }

    public static string ConnectionString =>
        CommandLineArgs.TryGetValue("ConnectionString", out string? value)
            ? value
            : DataBaseConfig.ConnectionString;

    public static string AdminPassword =>
        CommandLineArgs.TryGetValue("AdminPassword", out string? value)
            ? value
            : "admin123";

    public static string LogFilePath =>
        CommandLineArgs.TryGetValue("LogFilePath", out string? value) ? value : @"D:\CS_OOP_5_test.txt";

    public static bool EnableConsoleLogging =>
        CommandLineArgs.TryGetValue("EnableConsoleLogging", out string? value) && bool.Parse(value);

    public static bool EnableFileLogging =>
        CommandLineArgs.TryGetValue("EnableFileLogging", out string? value) && bool.Parse(value);
}