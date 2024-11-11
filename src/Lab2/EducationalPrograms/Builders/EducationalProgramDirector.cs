using Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms.EducationalProgramsRepository;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms.Builders;

public class EducationalProgramDirector<TEdProgram>
    where TEdProgram : BaseEducationalProgram, new()
{
    private readonly User user;

    private readonly EducationalProgramRepository repository;

    public EducationalProgramDirector(User customerUser, EducationalProgramRepository educationalProgramRepository)
    {
        user = customerUser;
        repository = educationalProgramRepository;
    }

    public TEdProgram BuildEducationalProgram(string programName, Collection<Tuple<BaseSubject, int>> subjects)
    {
        var builder = new EducationalProgramBuilder<TEdProgram>();
        TEdProgram program = builder
            .WithProgramName(programName)
            .WithProgramManager(user.UserName)
            .WithSubjects(subjects)
            .GetProgram();

        repository.Add(program);
        return program;
    }
}