using Itmo.ObjectOrientedProgramming.Lab2.Repository;

namespace Itmo.ObjectOrientedProgramming.Lab2.Users.UsersRepository;

public class UserRepository : BaseRepository<User>
{
    protected override Guid GetId(User item)
    {
        return item.Id;
    }
}