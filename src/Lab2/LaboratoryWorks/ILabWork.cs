using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.EvaluationCriteria;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks;

public interface ILabWork
{
    public Guid Id { get; set; }

    public Guid IdOfBasisLabWork { get; set; }

    public string? LabWorkName { get; set; }

    public string? AuthorName { get; set; }

    public string? Description { get; set; }

    public Collection<EvaluationCriterion>? EvaluationCriteria { get; }

    public int MaxPoints { get; set; }

    public void AddEvaluationCriteria(IEnumerable<EvaluationCriterion> evaluationCriteriaToAdd);

    public void RemoveEvaluationCriteria(IEnumerable<EvaluationCriterion> evaluationCriteriaToRemove);

    public void UpdateLabWork(
        User requestingUser,
        string? newLabWorkName = null,
        string? newDescription = null,
        Collection<EvaluationCriterion>? newEvaluationCriteria = null,
        IEnumerable<EvaluationCriterion>? newEvaluationCriteriaToAdd = null,
        IEnumerable<EvaluationCriterion>? evaluationCriteriaToRemove = null);
}