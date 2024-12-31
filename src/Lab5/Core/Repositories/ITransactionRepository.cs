using Itmo.ObjectOrientedProgramming.Lab5.Core.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Repositories;

public interface ITransactionRepository
{
    void Save(Transaction transaction);

    IReadOnlyCollection<Transaction> GetByAccountId(Guid accountId);
}