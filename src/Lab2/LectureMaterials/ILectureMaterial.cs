using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials;

public interface ILectureMaterial
{
    public Guid Id { get; set; }

    public Guid IdOfBasisLectureMaterial { get; set; }

    public string? LectureMaterialName { get; set; }

    public string? AuthorName { get; }

    public string? ShortDescription { get; set; }

    public string? Content { get; set; }

    public void UpdateLectureMaterial(
        User requestingUser,
        string? newLectureMaterialName = null,
        string? newShortDescription = null,
        string? newContent = null);
}