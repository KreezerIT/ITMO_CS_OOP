using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.EvaluationCriteria;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.Builders;

public interface ILabWorkBuilder
{
    public ILabWorkBuilder WithOwnID();

    public ILabWorkBuilder WithLabWorkName(string? labWorkName);

    public ILabWorkBuilder WithAuthorName(string? authorName);

    public ILabWorkBuilder WithDescription(string? description);

    public ILabWorkBuilder WithEvaluationCriteria(Collection<EvaluationCriterion>? evaluationCriteria);

    LabWork GetLabWork();
}