using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials.Builders;

public class LectureMaterialDirector
{
    private readonly User user;

    private readonly IRepository<LectureMaterial> repository;

    public LectureMaterialDirector(User customerUser, IRepository<LectureMaterial> lectureMaterialRepository)
    {
        user = customerUser;
        repository = lectureMaterialRepository;
    }

    public LectureMaterial BuildLectureMaterial(string lectureMaterialName, string shortDescription, string content)
    {
        var builder = new LectureMaterialBuilder();
        LectureMaterial lectureMaterial = builder
            .WithAuthorName(user.UserName)
            .WithOwnID()
            .WithLectureMaterialName(lectureMaterialName)
            .WithShortDescription(shortDescription)
            .WithContent(content)
            .GetLectureMaterial();

        repository.Add(lectureMaterial);
        return lectureMaterial;
    }
}