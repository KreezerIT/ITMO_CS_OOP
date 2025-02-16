namespace Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials.Builders;

public interface ILectureMaterialBuilder
{
    public ILectureMaterialBuilder WithOwnID();

    public ILectureMaterialBuilder WithLectureMaterialName(string? lectureMaterialName);

    public ILectureMaterialBuilder WithAuthorName(string? authorName);

    public ILectureMaterialBuilder WithShortDescription(string? shortDescription);

    public ILectureMaterialBuilder WithContent(string? content);

    LectureMaterial GetLectureMaterial();
}