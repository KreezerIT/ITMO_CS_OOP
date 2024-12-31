namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Interfaces;

public interface ILoginService
{
    bool LoginUser(string? accountNumber, string? password);

    bool LoginAdmin(string password);
}