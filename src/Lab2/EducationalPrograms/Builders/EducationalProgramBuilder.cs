using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms.Builders;

public class EducationalProgramBuilder<TEducationalProgram> : IEducationalProgramBuilder<TEducationalProgram>
    where TEducationalProgram : BaseEducationalProgram, new()
{
    private TEducationalProgram Program { get; } = new();

    public IEducationalProgramBuilder<TEducationalProgram> WithProgramName(string? educationalProgramName)
    {
        Program.EducationalProgramName = educationalProgramName ?? throw new ArgumentNullException(nameof(educationalProgramName), "Program name is null.");
        return this;
    }

    public IEducationalProgramBuilder<TEducationalProgram> WithProgramManager(string? programManager)
    {
        Program.ProgramManager = programManager ?? throw new ArgumentNullException(nameof(programManager), "Program manager is null.");
        return this;
    }

    public IEducationalProgramBuilder<TEducationalProgram> WithSubjects(Collection<Tuple<BaseSubject, int>> subjects)
    {
        Program.Subjects = subjects ?? throw new ArgumentNullException(nameof(subjects), "Subjects is null.");
        foreach (Tuple<BaseSubject, int> subject in subjects)
        {
            _ = subject ?? throw new ArgumentNullException(nameof(subjects), "Subject is null.");
            if (subject.Item2 < 1 || subject.Item2 > Program.TotalSemesters)
                throw new ArgumentOutOfRangeException(nameof(subjects), $"Semester should be between 1 and {Program.TotalSemesters}.");
        }

        return this;
    }

    public TEducationalProgram GetProgram()
    {
        return Program;
    }
}