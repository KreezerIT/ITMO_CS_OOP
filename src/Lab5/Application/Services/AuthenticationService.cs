using Itmo.ObjectOrientedProgramming.Lab5.Application.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Configs;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AccountService _accountService;

    private readonly string _adminPassword;

    public AuthenticationService(string adminPassword, AccountService accountService)
    {
        _accountService = accountService;
        _adminPassword = adminPassword ?? throw new InvalidInputException(nameof(adminPassword), "The admin password is not null.");
    }

    public AuthenticationService(AccountService accountService)
    {
        _accountService = accountService;
        _adminPassword = AppConfig.AdminPassword;
    }

    public bool AuthenticateUser(string accountNumber, string pin)
    {
        return _accountService.VerifyPin(accountNumber, pin);
    }

    public bool AuthenticateAdmin(string inputPassword)
    {
        return _adminPassword == inputPassword;
    }
}