using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public interface IEducationalProgram
{
    public Guid Id { get; }

    public string? EducationalProgramName { get; set; }

    public Collection<Tuple<BaseSubject, int>> Subjects { get; }

    public string? ProgramManager { get; set; }
}