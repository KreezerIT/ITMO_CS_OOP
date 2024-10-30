using Itmo.ObjectOrientedProgramming.Lab2.Repository;

namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public class User
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public User(string userName, IRepository<User> repository)
    {
        UserName = userName;
        Id = Guid.NewGuid();
        repository.Add(this);
    }

    public void UpdateUser(
        User requestingUser,
        string? newUserName = null)
    {
        if (UserName != requestingUser.UserName)
            throw new UnauthorizedAccessException("Only the User himself can update his account details.");

        if (newUserName != null)
            UserName = newUserName;
    }
}