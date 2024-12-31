namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Interfaces;

public interface IAuthenticationService
{
    bool AuthenticateUser(string accountNumber, string pin);

    bool AuthenticateAdmin(string inputPassword);
}