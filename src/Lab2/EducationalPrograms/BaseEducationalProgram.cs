using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public abstract class BaseEducationalProgram : IEducationalProgram
{
    public Guid Id { get; }

    public string? EducationalProgramName { get; set; }

    public int TotalSemesters { get; set; }

    public Collection<Tuple<BaseSubject, int>> Subjects { get; set; }

    public string? ProgramManager { get; set; }

    protected BaseEducationalProgram()
    {
        Subjects = new Collection<Tuple<BaseSubject, int>>();
    }

    public void AddSubjects(IEnumerable<Tuple<BaseSubject, int>> subjectsToAdd)
    {
        _ = Subjects ?? throw new InvalidOperationException("Subjects is not initialized.");

        foreach (Tuple<BaseSubject, int> subject in subjectsToAdd)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subjectsToAdd), "Subject cannot be null.");

            if (subject.Item2 < 1 || subject.Item2 > TotalSemesters)
                throw new ArgumentOutOfRangeException(nameof(subjectsToAdd), $"Subject {subject.Item1.SubjectName} is assigned to invalid semester {subject.Item2}. Allowed range is 1 to {TotalSemesters}.");

            Subjects.Add(subject);
        }
    }

    public void RemoveSubjects(IEnumerable<Tuple<BaseSubject, int>> subjectsToRemove)
    {
        foreach (Tuple<BaseSubject, int> subject in subjectsToRemove)
        {
            if (!Subjects.Contains(subject) || !Subjects.Remove(subject))
            {
                throw new InvalidOperationException($"Subject {subject.Item1.SubjectName} not found in the program.");
            }
        }
    }

    public void UpdateEducationalProgram(
        User requestingUser,
        string? newCreditSubjectName = null,
        Collection<Tuple<BaseSubject, int>>? newSubjects = null,
        IEnumerable<Tuple<BaseSubject, int>>? newSubjectsToAdd = null,
        IEnumerable<Tuple<BaseSubject, int>>? subjectsToRemove = null)
    {
        if (ProgramManager != requestingUser.UserName)
            throw new UnauthorizedAccessException("Only the ProgramManager can update this EducationalProgram.");

        if (newSubjects != null)
            Subjects = newSubjects;

        if (newSubjectsToAdd != null)
            AddSubjects(newSubjectsToAdd);

        if (subjectsToRemove != null)
            RemoveSubjects(subjectsToRemove);
    }
}