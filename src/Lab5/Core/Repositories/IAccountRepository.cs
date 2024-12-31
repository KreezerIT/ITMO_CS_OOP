using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;

public interface IAccountRepository
{
    Account? GetByAccountNumber(string accountNumber);

    void Save(Account account);

    void Update(Account account);

    void Delete(string accountNumber);

    bool VerifyAccountPin(string accountNumber, string pin);
}