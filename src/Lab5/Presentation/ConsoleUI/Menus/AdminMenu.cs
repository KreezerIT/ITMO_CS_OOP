using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.ConsoleUI.Menus;

public class AdminMenu
{
    private readonly AccountService _accountService;

    public AdminMenu(AccountService accountService)
    {
        _accountService = accountService;
    }

    public void Show(LoginService loginService)
    {
        while (true)
        {
            Console.WriteLine("\nEnter system password: ");
            string? password = Console.ReadLine();
            ArgumentException.ThrowIfNullOrEmpty(password);

            loginService.LoginAdmin(password);
            Console.WriteLine("\nAdmin mode activated");

            Console.WriteLine("\n--- Admin Menu ---");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Check Account Balance");
            Console.WriteLine("3. Exit");
            Console.Write("Your choice: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    Console.WriteLine("Enter AccountNumber:");
                    string accountNumber = ValidateInput.GetValidatedInput(@"^\d{20}$", "Invalid account number! It must be exactly 20 digits.");

                    ViewBalanceByAccountNumber(accountNumber);
                    break;
                case "3":
                    Console.WriteLine("Exiting admin mode...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    private void CreateAccount()
    {
        Console.Write("Enter 20-digit account number: ");
        string accountNumber = ValidateInput.GetValidatedInput(@"^\d{20}$", "Invalid account number! It must be exactly 20 digits.");

        Console.Write("Enter PIN code in **** format: ");
        string pinCode = ValidateInput.GetValidatedInput(@"^\d{4}$", "Invalid PIN code! It must be exactly 4 digits.");

        Console.Write("Enter currency in XXX format: ");
        string currency = ValidateInput.GetValidatedInput(@"^[A-Z]{3}$", "Invalid currency! It must be exactly 3 uppercase letters.");

        _accountService.CreateAccount(accountNumber, pinCode, currency);
        Console.WriteLine("Account created successfully!");
    }

    private void ViewBalanceByAccountNumber(string accountNumber)
    {
        try
        {
            string balance = _accountService.GetBalance(accountNumber);
            Console.WriteLine($"Account number: {accountNumber} Balance: {balance}");
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}