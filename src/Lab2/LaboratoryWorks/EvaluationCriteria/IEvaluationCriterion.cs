namespace Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.EvaluationCriteria;

public interface IEvaluationCriterion
{
    public string? CriterionName { get; set; }

    public int CriterionMaxPoints { get; set; }
}