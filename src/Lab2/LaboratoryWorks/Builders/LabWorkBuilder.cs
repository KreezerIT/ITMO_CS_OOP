using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.EvaluationCriteria;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.Builders;

public class LabWorkBuilder : ILabWorkBuilder
{
    private LabWork BuildingLabWork { get; set; } = new();

    public ILabWorkBuilder WithOwnID()
    {
        BuildingLabWork.Id = Guid.NewGuid();
        return this;
    }

    public ILabWorkBuilder WithLabWorkName(string? labWorkName)
    {
        BuildingLabWork.LabWorkName = labWorkName ?? throw new ArgumentNullException(nameof(labWorkName), "LabWorkName is null.");
        return this;
    }

    public ILabWorkBuilder WithAuthorName(string? authorName)
    {
        BuildingLabWork.AuthorName = authorName ?? throw new ArgumentNullException(nameof(authorName), "AuthorName is null.");
        return this;
    }

    public ILabWorkBuilder WithDescription(string? description)
    {
        BuildingLabWork.Description = description ?? throw new ArgumentNullException(nameof(description), "Description is null.");
        return this;
    }

    public ILabWorkBuilder WithEvaluationCriteria(Collection<EvaluationCriterion>? evaluationCriteria)
    {
        _ = evaluationCriteria ?? throw new ArgumentNullException(nameof(evaluationCriteria), "EvaluationCriteria is null.");

        foreach (EvaluationCriterion criterion in evaluationCriteria)
        {
            _ = criterion ?? throw new ArgumentNullException(nameof(evaluationCriteria), "evaluationCriterion have a criterion that is null.");
            ArgumentException.ThrowIfNullOrEmpty(criterion.CriterionName, nameof(criterion.CriterionName));
            _ = criterion.CriterionMaxPoints > 0
                ? criterion.CriterionMaxPoints
                : throw new ArgumentException("CriterionMaxPoints must be greater than zero.");
            BuildingLabWork.MaxPoints += criterion.CriterionMaxPoints;
        }

        BuildingLabWork.EvaluationCriteria = evaluationCriteria;
        return this;
    }

    public LabWork GetLabWork()
    {
        return BuildingLabWork;
    }
}