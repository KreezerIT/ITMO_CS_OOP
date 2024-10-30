using Itmo.ObjectOrientedProgramming.Lab2.Prototype;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.SubjectTypes;

public class ExamSubject : BaseSubject, IPrototype<ExamSubject>
{
    public int MaximumPointsForExam { get; set; }

    public ExamSubject Clone(User user)
    {
        return (ExamSubject)CloneBase(user);
    }

    protected internal override void ValidateTotalPoints()
    {
        if (TotalSubjectPointsForWorks + MaximumPointsForExam != 100)
            throw new InvalidOperationException("TotalExamSubjectPoints must equal 100.");
    }
}