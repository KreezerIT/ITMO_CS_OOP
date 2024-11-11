using Itmo.ObjectOrientedProgramming.Lab2.Repository;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms.EducationalProgramsRepository;

public class EducationalProgramRepository : BaseRepository<IEducationalProgram>
{
    protected override Guid GetId(IEducationalProgram item)
    {
        return item.Id;
    }
}