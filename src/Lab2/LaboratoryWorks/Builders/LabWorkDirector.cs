using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.EvaluationCriteria;
using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.Builders;

public class LabWorkDirector
{
    private readonly User user;

    private readonly IRepository<LabWork> repository;

    public LabWorkDirector(User customerUser, IRepository<LabWork> labWorkRepository)
    {
        user = customerUser;
        repository = labWorkRepository;
    }

    public LabWork BuildLabWork(string labWorkName, string description, Collection<EvaluationCriterion> evaluationCriteria)
    {
        var builder = new LabWorkBuilder();

        LabWork labWork = builder
            .WithAuthorName(user.UserName)
            .WithOwnID()
            .WithLabWorkName(labWorkName)
            .WithDescription(description)
            .WithEvaluationCriteria(evaluationCriteria)
            .GetLabWork();

        repository.Add(labWork);
        return labWork;
    }
}