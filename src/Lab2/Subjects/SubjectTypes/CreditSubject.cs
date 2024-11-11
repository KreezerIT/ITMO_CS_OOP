using Itmo.ObjectOrientedProgramming.Lab2.Prototype;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.SubjectTypes;

public class CreditSubject : BaseSubject, IPrototype<CreditSubject>
{
    public int MinimumPointsToCompleteCreditSubject { get; set; }

    public int MaximumPointsForCredit { get; set; }

    public CreditSubject Clone(User user)
    {
        return (CreditSubject)CloneBase(user);
    }

    protected internal override void ValidateTotalPoints()
    {
        if (TotalSubjectPointsForWorks + MaximumPointsForCredit != 100)
            throw new InvalidOperationException("TotalCreditSubjectPoints must equal 100.");
    }
}