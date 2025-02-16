using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms.Builders;

public interface IEducationalProgramBuilder<TEducationalProgram>
{
    IEducationalProgramBuilder<TEducationalProgram> WithProgramName(string? educationalProgramName);

    IEducationalProgramBuilder<TEducationalProgram> WithProgramManager(string? programManager);

    IEducationalProgramBuilder<TEducationalProgram> WithSubjects(Collection<Tuple<BaseSubject, int>> subjects);

    TEducationalProgram GetProgram();
}