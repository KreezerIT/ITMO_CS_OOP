using Itmo.ObjectOrientedProgramming.Lab5.Core.ValueObjects;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ConsoleUI.Menus;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.ConsoleUI;

public class ATMConsoleApp
{
    private readonly AdminMenu _adminMenu;

    private readonly UserMenu _userMenu;

    private readonly LoginService _loginService;

    public ATMConsoleApp(AdminMenu adminMenu, UserMenu userMenu, LoginService loginService)
    {
        _adminMenu = adminMenu;
        _userMenu = userMenu;
        _loginService = loginService;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the ITMO-ATM!");
        Console.WriteLine("Please choose a mode:");
        Console.WriteLine($"1. {UserMods.Client}");
        Console.WriteLine($"2. {UserMods.Admin}");
        Console.Write("Your choice: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _userMenu.Show(_loginService);
                break;
            case "2":
                _adminMenu.Show(_loginService);
                break;
            default:
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }
}