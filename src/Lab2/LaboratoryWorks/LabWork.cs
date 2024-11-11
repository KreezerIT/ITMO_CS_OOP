using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.EvaluationCriteria;
using Itmo.ObjectOrientedProgramming.Lab2.Prototype;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks;

public class LabWork : ILabWork, IPrototype<LabWork>
{
    public Guid Id { get; set; }

    public Guid IdOfBasisLabWork { get; set; }

    public string? LabWorkName { get; set; }

    public string? AuthorName { get; set; }

    public string? Description { get; set; }

    public Collection<EvaluationCriterion>? EvaluationCriteria { get; set; }

    public int MaxPoints { get; set; }

    public void AddEvaluationCriteria(IEnumerable<EvaluationCriterion> evaluationCriteriaToAdd)
    {
        ArgumentNullException.ThrowIfNull(evaluationCriteriaToAdd, nameof(evaluationCriteriaToAdd));
        _ = EvaluationCriteria ?? throw new InvalidOperationException("EvaluationCriteria is not initialized.");

        foreach (EvaluationCriterion criterion in evaluationCriteriaToAdd)
        {
            ArgumentNullException.ThrowIfNull(criterion, nameof(criterion));
            EvaluationCriteria.Add(criterion);
            MaxPoints += criterion.CriterionMaxPoints;
        }
    }

    public void RemoveEvaluationCriteria(IEnumerable<EvaluationCriterion> evaluationCriteriaToRemove)
    {
        ArgumentNullException.ThrowIfNull(evaluationCriteriaToRemove, nameof(evaluationCriteriaToRemove));
        _ = EvaluationCriteria ?? throw new InvalidOperationException("EvaluationCriteria is not initialized.");

        foreach (EvaluationCriterion criterion in evaluationCriteriaToRemove)
        {
            ArgumentNullException.ThrowIfNull(criterion, nameof(criterion));
            if (!EvaluationCriteria.Remove(criterion))
            {
                throw new InvalidOperationException($"EvaluationCriterion {criterion.CriterionName} is not found.");
            }

            MaxPoints -= criterion.CriterionMaxPoints;
        }
    }

    public void UpdateLabWork(
        User requestingUser,
        string? newLabWorkName = null,
        string? newDescription = null,
        Collection<EvaluationCriterion>? newEvaluationCriteria = null,
        IEnumerable<EvaluationCriterion>? newEvaluationCriteriaToAdd = null,
        IEnumerable<EvaluationCriterion>? evaluationCriteriaToRemove = null)
    {
        if (AuthorName != requestingUser.UserName)
        {
            throw new UnauthorizedAccessException("Only the author can update this lab work.");
        }

        if (!string.IsNullOrEmpty(newLabWorkName))
        {
            LabWorkName = newLabWorkName;
        }

        if (!string.IsNullOrEmpty(newDescription))
        {
            Description = newDescription;
        }

        if (newEvaluationCriteria != null)
        {
            EvaluationCriteria = newEvaluationCriteria;
        }

        if (newEvaluationCriteriaToAdd != null)
        {
            _ = EvaluationCriteria ?? throw new InvalidOperationException("EvaluationCriteria is not initialized.");
            AddEvaluationCriteria(newEvaluationCriteriaToAdd);
        }

        if (evaluationCriteriaToRemove != null)
        {
            _ = EvaluationCriteria ?? throw new InvalidOperationException("EvaluationCriteria is not initialized.");
            RemoveEvaluationCriteria(evaluationCriteriaToRemove);
        }
    }

    public LabWork Clone(User user)
    {
        var copy = (LabWork)MemberwiseClone();

        copy.EvaluationCriteria = EvaluationCriteria == null
            ? []
            : new Collection<EvaluationCriterion>(
                EvaluationCriteria.Select(criterion => new EvaluationCriterion
                {
                    CriterionName = criterion.CriterionName,
                    CriterionMaxPoints = criterion.CriterionMaxPoints,
                }).ToList());

        AuthorName = user.UserName;
        copy.IdOfBasisLabWork = Id;
        return copy;
    }
}