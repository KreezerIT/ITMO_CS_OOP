using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public interface ISubject
{
    public Guid Id { get; }

    public Guid IdOfBasisSubject { get; set; }

    public string? SubjectName { get; }

    public string? AuthorName { get; }

    public int TotalSubjectPointsForWorks { get; }

    public Collection<LabWork>? LabWorks { get; }

    public Collection<LectureMaterial>? LecMaterials { get; }
}
