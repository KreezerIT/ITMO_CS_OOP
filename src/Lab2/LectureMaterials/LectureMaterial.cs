using Itmo.ObjectOrientedProgramming.Lab2.Prototype;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials;

public class LectureMaterial : ILectureMaterial, IPrototype<LectureMaterial>
{
    public Guid Id { get; set; }

    public Guid IdOfBasisLectureMaterial { get; set; }

    public string? LectureMaterialName { get; set; }

    public string? AuthorName { get; set; }

    public string? ShortDescription { get; set; }

    public string? Content { get; set; }

    public void UpdateLectureMaterial(
        User requestingUser,
        string? newLectureMaterialName = null,
        string? newShortDescription = null,
        string? newContent = null)
    {
        if (AuthorName != requestingUser.UserName)
        {
            throw new UnauthorizedAccessException("Only the author can update this lecture material.");
        }

        if (!string.IsNullOrEmpty(newLectureMaterialName))
        {
            LectureMaterialName = newLectureMaterialName;
        }

        if (!string.IsNullOrEmpty(newShortDescription))
        {
            ShortDescription = newShortDescription;
        }

        if (!string.IsNullOrEmpty(newContent))
        {
            Content = newContent;
        }
    }

    public LectureMaterial Clone(User user)
    {
        var copy = (LectureMaterial)this.MemberwiseClone();

        AuthorName = user.UserName;
        copy.IdOfBasisLectureMaterial = Id;
        return copy;
    }
}