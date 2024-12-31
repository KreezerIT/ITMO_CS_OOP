using Itmo.ObjectOrientedProgramming.Lab5.Application.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public class IdentificationService : IIdentificationService
{
    public string? Identifier { get; set; }

    private readonly AccountService _accountService;

    public IdentificationService(AccountService accountService)
    {
        _accountService = accountService;
    }

    public bool IsRegistered(string identifier)
    {
        return _accountService.CheckByAccountNumber(identifier);
    }
}