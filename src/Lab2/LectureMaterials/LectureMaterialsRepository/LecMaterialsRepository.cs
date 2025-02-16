using Itmo.ObjectOrientedProgramming.Lab2.Repository;

namespace Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials.LectureMaterialsRepository;

public class LecMaterialsRepository : BaseRepository<LectureMaterial>
{
    protected override Guid GetId(LectureMaterial item)
    {
        return item.Id;
    }
}