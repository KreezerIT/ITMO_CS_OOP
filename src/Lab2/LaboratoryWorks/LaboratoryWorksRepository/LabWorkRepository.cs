using Itmo.ObjectOrientedProgramming.Lab2.Repository;

namespace Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.LaboratoryWorksRepository;

public class LabWorkRepository : BaseRepository<LabWork>
{
    protected override Guid GetId(LabWork item)
    {
        return item.Id;
    }
}