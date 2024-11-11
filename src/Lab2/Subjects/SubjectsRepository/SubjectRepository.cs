using Itmo.ObjectOrientedProgramming.Lab2.Repository;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.SubjectsRepository;

public class SubjectRepository : BaseRepository<ISubject>
{
    protected override Guid GetId(ISubject item)
    {
        return item.Id;
    }
}