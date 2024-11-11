using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;

public interface ISubjectBuilder<TSubject>
    where TSubject : BaseSubject
{
    ISubjectBuilder<TSubject> WithSubjectName(string? subjectName);

    ISubjectBuilder<TSubject> WithAuthorName(string? authorName);

    ISubjectBuilder<TSubject> WithLabWorks(Collection<LabWork> labWorks);

    ISubjectBuilder<TSubject> WithLectureMaterials(Collection<LectureMaterial> lecMaterials);

    ISubjectBuilder<TSubject> WithCreditPoints(int minPointsToComplete, int maxPointsForCredit);

    ISubjectBuilder<TSubject> WithExamPoints(int maxPointsForExam);

    TSubject GetSubject();
}