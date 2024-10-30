using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks;
using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.EvaluationCriteria;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public abstract class BaseSubject : ISubject
{
    public Guid Id { get; }

    public Guid IdOfBasisSubject { get; set; }

    public string? SubjectName { get; set; }

    public string? AuthorName { get; set; }

    public int TotalSubjectPointsForWorks { get; set; }

    public Collection<LabWork>? LabWorks { get; set; }

    public Collection<LectureMaterial>? LecMaterials { get; set; }

    protected BaseSubject()
    {
        LabWorks = new Collection<LabWork>();
        LecMaterials = new Collection<LectureMaterial>();
    }

    public void AddLabWorks(IEnumerable<LabWork> labWorksToAdd)
    {
        _ = LabWorks ?? throw new InvalidOperationException("LabWorks is not initialized.");

        foreach (LabWork labWork in labWorksToAdd)
        {
            ArgumentNullException.ThrowIfNull(labWork, nameof(labWork));
            ArgumentNullException.ThrowIfNull(labWork.EvaluationCriteria, nameof(labWork.EvaluationCriteria));

            foreach (EvaluationCriterion criterion in labWork.EvaluationCriteria)
            {
                ArgumentNullException.ThrowIfNull(nameof(criterion), "evaluationCriterion have a criterion that is null.");
                ArgumentException.ThrowIfNullOrEmpty(criterion.CriterionName, nameof(criterion.CriterionName));
                _ = criterion.CriterionMaxPoints > 0
                    ? criterion.CriterionMaxPoints
                    : throw new ArgumentException("CriterionMaxPoints must be greater than zero.");
            }

            LabWorks.Add(labWork);
            TotalSubjectPointsForWorks += labWork.MaxPoints;
        }
    }

    public void RemoveLabWorks(IEnumerable<LabWork> labWorksToRemove)
    {
        _ = LabWorks ?? throw new InvalidOperationException("LabWorks is not initialized.");

        foreach (LabWork labWork in labWorksToRemove)
        {
            ArgumentNullException.ThrowIfNull(labWork, nameof(labWork));
            if (!LabWorks.Contains(labWork) || !LabWorks.Remove(labWork))
            {
                throw new InvalidOperationException($"LabWork {labWork.LabWorkName} is not found in the current subject.");
            }

            TotalSubjectPointsForWorks -= labWork.MaxPoints;
        }
    }

    public void AddLectureMaterials(IEnumerable<LectureMaterial> lectureMaterialsToAdd)
    {
        _ = LecMaterials ?? throw new InvalidOperationException("LecMaterials is not initialized.");

        foreach (LectureMaterial lectureMaterial in lectureMaterialsToAdd)
        {
            ArgumentNullException.ThrowIfNull(lectureMaterial, nameof(lectureMaterial));
            LecMaterials.Add(lectureMaterial);
        }
    }

    public void RemoveLectureMaterials(IEnumerable<LectureMaterial> lectureMaterialsToRemove)
    {
        _ = LecMaterials ?? throw new InvalidOperationException("LecMaterials is not initialized.");

        foreach (LectureMaterial lectureMaterial in lectureMaterialsToRemove)
        {
            ArgumentNullException.ThrowIfNull(lectureMaterial, nameof(lectureMaterial));
            if (!LecMaterials.Contains(lectureMaterial) || !LecMaterials.Remove(lectureMaterial))
                throw new InvalidOperationException($"LectureMaterial {lectureMaterial.LectureMaterialName} is not found.");
        }
    }

    public void UpdateSubject(
        User requestingUser,
        string? newSubjectName = null,
        Collection<LabWork>? newLabWorks = null,
        IEnumerable<LabWork>? newLabWorksToAdd = null,
        IEnumerable<LabWork>? labWorksToRemove = null,
        Collection<LectureMaterial>? newLecMaterials = null,
        IEnumerable<LectureMaterial>? newLectureMaterialToAdd = null,
        IEnumerable<LectureMaterial>? lectureMaterialsToRemove = null)
    {
        if (AuthorName != requestingUser.UserName)
            throw new UnauthorizedAccessException("Only the author can update this subject.");

        if (!string.IsNullOrEmpty(newSubjectName))
            SubjectName = newSubjectName;

        if (newLabWorks != null)
            LabWorks = newLabWorks;

        if (newLecMaterials != null)
            LecMaterials = newLecMaterials;

        if (newLabWorksToAdd != null)
        {
            AddLabWorks(newLabWorksToAdd);
        }

        if (labWorksToRemove != null)
        {
            RemoveLabWorks(labWorksToRemove);
        }

        if (newLectureMaterialToAdd != null)
        {
            AddLectureMaterials(newLectureMaterialToAdd);
        }

        if (lectureMaterialsToRemove != null)
        {
            RemoveLectureMaterials(lectureMaterialsToRemove);
        }

        ValidateTotalPoints();
    }

    public BaseSubject CloneBase(User user)
    {
        var copy = (BaseSubject)MemberwiseClone();
        copy.LabWorks = LabWorks != null
            ? new Collection<LabWork>(LabWorks.Select(labWork => labWork.Clone(user)).ToList())
            : null;
        copy.LecMaterials = LecMaterials != null
            ? new Collection<LectureMaterial>(LecMaterials.Select(lectureMaterial => lectureMaterial.Clone(user)).ToList())
            : null;

        AuthorName = user.UserName;
        copy.IdOfBasisSubject = Id;
        return copy;
    }

    protected internal abstract void ValidateTotalPoints();
}