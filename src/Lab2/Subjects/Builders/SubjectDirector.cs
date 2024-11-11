using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.SubjectsRepository;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.SubjectTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;

public class SubjectDirector
{
    private readonly User user;

    private readonly SubjectRepository repository;

    public SubjectDirector(User customerUser, SubjectRepository subjectRepository)
    {
        user = customerUser;
        repository = subjectRepository;
    }

    public ExamSubject BuildExamSubject(string subjectName, int examPoints, Collection<LabWork> labWorks, Collection<LectureMaterial> lectureMaterials)
    {
        var builder = new SubjectBuilder<ExamSubject>();
        ExamSubject subject = builder
            .WithSubjectName(subjectName)
            .WithAuthorName(user.UserName)
            .WithLabWorks(labWorks)
            .WithLectureMaterials(lectureMaterials)
            .WithExamPoints(examPoints)
            .GetSubject();

        repository.Add(subject);
        return subject;
    }

    public CreditSubject BuildCreditSubject(string subjectName, int minPointsToComplete, int maxPoints, Collection<LabWork> labWorks, Collection<LectureMaterial> lectureMaterials)
    {
        var builder = new SubjectBuilder<CreditSubject>();

        CreditSubject subject = builder
            .WithSubjectName(subjectName)
            .WithAuthorName(user.UserName)
            .WithLabWorks(labWorks)
            .WithLectureMaterials(lectureMaterials)
            .WithCreditPoints(minPointsToComplete, maxPoints)
            .GetSubject();

        repository.Add(subject);
        return subject;
    }
}