using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ViewModels;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.ConsoleUI.Menus;

public class UserMenu
    {
        private readonly AccountService _accountService;

        public UserMenu(AccountService accountService)
        {
            _accountService = accountService;
        }

        public void Show(LoginService loginService)
        {
            Console.Write("Enter 20-digit account number: ");
            string accountNumber = ValidateInput.GetValidatedInput(@"^\d{20}$", "Invalid account number! It must be exactly 20 digits.");

            Console.Write("Enter PIN code in **** format: ");
            string pinCode = ValidateInput.GetValidatedInput(@"^\d{4}$", "Invalid PIN code! It must be exactly 4 digits.");

            loginService.LoginUser(accountNumber, pinCode);
            while (true)
            {
                Console.WriteLine("\n--- User Menu ---");
                Console.WriteLine("1. View Balance");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. View Transaction History");
                Console.WriteLine("5. Exit");
                Console.Write("Your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewBalance(accountNumber);
                        break;
                    case "2":
                        DepositMoney(accountNumber);
                        break;
                    case "3":
                        WithdrawMoney(accountNumber);
                        break;
                    case "4":
                        ViewTransactionHistory(accountNumber);
                        break;
                    case "5":
                        Console.WriteLine("Exiting user mode...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private void ViewBalance(string accountNumber)
        {
            try
            {
                string balance = _accountService.GetBalance(accountNumber);
                Console.WriteLine($"Your current balance: {balance}");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void DepositMoney(string accountNumber)
        {
            try
            {
                Console.Write("Enter amount to deposit: ");
                string amountInput = ValidateInput.GetValidatedInput(
                    @"^\d+(\.\d{1,2})?$",
                    "Invalid amount! It must be a positive number with up to two decimal places.");

                Console.Write("Enter currency (e.g., USD, EUR): ");
                string currency = ValidateInput.GetValidatedInput(@"^[A-Z]{3}$", "Invalid currency! It must be exactly 3 uppercase letters.");
                string accountCurrency = _accountService.GetBalance(accountNumber).Split(' ')[1];
                if (currency != accountCurrency) throw new FormatException("Invalid currency format!");
                var money = new Money(decimal.Parse(amountInput), Enum.Parse<CurrencyTypes>(currency ?? string.Empty));

                _accountService.Deposit(accountNumber, money);
                Console.WriteLine("Money deposited successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void WithdrawMoney(string accountNumber)
        {
            try
            {
                Console.Write("Enter amount to withdraw: ");
                string amountInput = ValidateInput.GetValidatedInput(
                    @"^\d+(\.\d{1,2})?$",
                    "Invalid amount! It must be a positive number with up to two decimal places.");

                _accountService.Withdraw(accountNumber, decimal.Parse(amountInput));
                Console.WriteLine("Money withdrawn successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void ViewTransactionHistory(string accountNumber)
        {
            try
            {
                IReadOnlyCollection<TransactionViewModel> transactions = _accountService.GetAccountTransactionsByViewModel(accountNumber);
                Console.WriteLine("\n--- Transaction History ---");

                foreach (TransactionViewModel transaction in transactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
}