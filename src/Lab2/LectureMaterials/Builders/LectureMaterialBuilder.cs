namespace Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials.Builders;

public class LectureMaterialBuilder : ILectureMaterialBuilder
{
    private LectureMaterial BuildingLectureMaterial { get; } = new();

    public ILectureMaterialBuilder WithOwnID()
    {
        BuildingLectureMaterial.Id = Guid.NewGuid();
        return this;
    }

    public ILectureMaterialBuilder WithLectureMaterialName(string? lectureMaterialName)
    {
        BuildingLectureMaterial.LectureMaterialName = lectureMaterialName ?? throw new ArgumentNullException(nameof(lectureMaterialName), "LectureMaterialName is null.");
        return this;
    }

    public ILectureMaterialBuilder WithAuthorName(string? authorName)
    {
        BuildingLectureMaterial.AuthorName = authorName ?? throw new ArgumentNullException(nameof(authorName), "AuthorName is null.");
        return this;
    }

    public ILectureMaterialBuilder WithShortDescription(string? shortDescription)
    {
        BuildingLectureMaterial.ShortDescription = shortDescription ?? throw new ArgumentNullException(nameof(shortDescription), "Description is null.");
        return this;
    }

    public ILectureMaterialBuilder WithContent(string? content)
    {
        BuildingLectureMaterial.ShortDescription = content ?? throw new ArgumentNullException(nameof(content), "Description is null.");
        return this;
    }

    public LectureMaterial GetLectureMaterial()
    {
        return BuildingLectureMaterial;
    }
}