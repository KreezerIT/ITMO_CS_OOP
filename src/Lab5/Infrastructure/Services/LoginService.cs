using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Services;

public class LoginService : ILoginService
{
    private readonly AccountService _accountService;

    private readonly IdentificationService _identificationService;

    private readonly AuthenticationService _authenticationService;

    public LoginService(AccountService accountService, IdentificationService identificationService, AuthenticationService authenticationService)
    {
        _accountService = accountService;
        _identificationService = identificationService;
        _authenticationService = authenticationService;
    }

    public LoginService(AccountService accountService)
    {
        _accountService = accountService;
        _identificationService = new IdentificationService(_accountService);
        _authenticationService = new AuthenticationService(_accountService);
    }

    public bool LoginUser(string? accountNumber, string? password)
    {
        ArgumentNullException.ThrowIfNull(accountNumber);
        ArgumentNullException.ThrowIfNull(password);

        if (!_identificationService.IsRegistered(accountNumber)) return false;
        if (!_authenticationService.AuthenticateUser(accountNumber, password)) return false;

        return true;
    }

    public bool LoginAdmin(string password)
    {
        ArgumentNullException.ThrowIfNull(password);

        if (!_authenticationService.AuthenticateAdmin(password)) return false;

        return true;
    }
}