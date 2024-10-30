using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.SubjectTypes;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;

public class SubjectBuilder<TSubject> : ISubjectBuilder<TSubject>
        where TSubject : BaseSubject, new()
{
    private TSubject Subject { get; } = new();

    public ISubjectBuilder<TSubject> WithSubjectName(string? subjectName)
    {
        Subject.SubjectName = subjectName ?? throw new ArgumentNullException(nameof(subjectName), "subjectName is null.");
        return this;
    }

    public ISubjectBuilder<TSubject> WithAuthorName(string? authorName)
    {
        Subject.AuthorName = authorName ?? throw new ArgumentNullException(nameof(authorName), "authorName is null.");
        return this;
    }

    public ISubjectBuilder<TSubject> WithLabWorks(Collection<LabWork> labWorks)
    {
        Subject.LabWorks = labWorks ?? throw new ArgumentNullException(nameof(labWorks), "labWorks is null.");

        foreach (LabWork labWork in labWorks)
        {
            _ = labWork ?? throw new ArgumentNullException(nameof(labWorks), "labWorks have a criterion that is null.");
            Subject.TotalSubjectPointsForWorks += labWork.MaxPoints;
        }

        return this;
    }

    public ISubjectBuilder<TSubject> WithLectureMaterials(Collection<LectureMaterial> lecMaterials)
    {
        Subject.LecMaterials = lecMaterials ?? throw new ArgumentNullException(nameof(lecMaterials), "lecMaterials is null.");

        foreach (LectureMaterial lecMaterial in lecMaterials)
        {
            _ = lecMaterial ?? throw new ArgumentNullException(nameof(lecMaterials), "lecMaterials have a lecMaterial that is null.");
        }

        return this;
    }

    public ISubjectBuilder<TSubject> WithCreditPoints(int minPointsToComplete, int maxPointsForCredit)
    {
        if (Subject is not CreditSubject creditSubject)
        {
            throw new InvalidOperationException("Wrong method for this subject type.");
        }

        creditSubject.MaximumPointsForCredit = maxPointsForCredit;
        creditSubject.MinimumPointsToCompleteCreditSubject = minPointsToComplete;
        return this;
    }

    public ISubjectBuilder<TSubject> WithExamPoints(int maxPointsForExam)
    {
        if (Subject is not ExamSubject examSubject)
        {
            throw new InvalidOperationException("Wrong method for this subject type.");
        }

        examSubject.MaximumPointsForExam = maxPointsForExam;
        return this;
    }

    public TSubject GetSubject()
    {
        Subject.ValidateTotalPoints();
        return Subject;
    }
}