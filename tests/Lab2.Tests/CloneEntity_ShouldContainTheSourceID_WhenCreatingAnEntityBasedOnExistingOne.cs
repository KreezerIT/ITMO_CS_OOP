using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks;
using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.EvaluationCriteria;
using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.LaboratoryWorksRepository;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials.LectureMaterialsRepository;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.SubjectsRepository;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using Itmo.ObjectOrientedProgramming.Lab2.Users.UsersRepository;
using System.Collections.ObjectModel;
using Xunit;

namespace Lab2.Tests;

public class CloneEntity_ShouldContainTheSourceID_WhenCreatingAnEntityBasedOnExistingOne
{
    [Fact]
    public void LabWork_CheckCloneContainBasisID()
    {
        // Arrange
        var users = new UserRepository();
        var labWorks = new LabWorkRepository();

        var userOne = new User("Aboba", users);
        var labWorkOneDirector = new LabWorkDirector(userOne, labWorks);
        LabWork labWorkOne = labWorkOneDirector.BuildLabWork(
            "Opa",
            "labydi labydai",
            new Collection<EvaluationCriterion>
            {
                new EvaluationCriterion { CriterionName = "Четкость", CriterionMaxPoints = 10 },
                new EvaluationCriterion { CriterionName = "Яркость", CriterionMaxPoints = 15 },
            });

        // Act
        LabWork labWorkOnePrototype = labWorkOne.Clone(userOne);

        // Assert
        Assert.Equal(labWorkOne.Id, labWorkOnePrototype.IdOfBasisLabWork);
    }

    [Fact]
    public void LectureMaterial_CheckCloneContainBasisID()
    {
        // Arrange
        var users = new UserRepository();
        var lectureMaterials = new LecMaterialsRepository();

        var userOne = new User("Aboba", users);
        var lectureMaterialOneDirector = new LectureMaterialDirector(userOne, lectureMaterials);
        LectureMaterial lectureMaterialOne = lectureMaterialOneDirector.BuildLectureMaterial(
            "Opa",
            "labydi labydai",
            "mega super content");

        // Act
        LectureMaterial lectureMaterialOnePrototype = lectureMaterialOne.Clone(userOne);

        // Assert
        Assert.Equal(lectureMaterialOne.Id, lectureMaterialOnePrototype.IdOfBasisLectureMaterial);
    }

    [Fact]
    public void Subject_CheckCloneContainBasisID()
    {
        // Arrange
        var users = new UserRepository();
        var subjects = new SubjectRepository();
        var labWorks = new LabWorkRepository();
        var lectureMaterials = new LecMaterialsRepository();

        var userOne = new User("Aboba", users);

        var labWorkOneDirector = new LabWorkDirector(userOne, labWorks);
        var labWorksCollectionOne = new Collection<LabWork>
        {
            labWorkOneDirector.BuildLabWork(
                "Opa",
                "labydi labydai",
                new Collection<EvaluationCriterion>
                {
                    new EvaluationCriterion { CriterionName = "Четкость", CriterionMaxPoints = 10 },
                    new EvaluationCriterion { CriterionName = "Яркость", CriterionMaxPoints = 15 },
                }),
            labWorkOneDirector.BuildLabWork(
                "Nepon",
                "da ya",
                new Collection<EvaluationCriterion>
                {
                    new EvaluationCriterion { CriterionName = "Жесткость", CriterionMaxPoints = 20 },
                    new EvaluationCriterion { CriterionName = "Правописание", CriterionMaxPoints = 25 },
                }),
        };

        var lectureMaterialOneDirector = new LectureMaterialDirector(userOne, lectureMaterials);
        var lectureMaterialsCollectionOne = new Collection<LectureMaterial>
        {
            lectureMaterialOneDirector.BuildLectureMaterial(
                "OpLop",
                "labydi labydai",
                "mega super content"),
        };

        var subjectOneDirector = new SubjectDirector(userOne, subjects);
        BaseSubject subjectOne = subjectOneDirector.BuildExamSubject(
            "Algorithms",
            30,
            labWorksCollectionOne,
            lectureMaterialsCollectionOne);

        BaseSubject subjectTwo = subjectOneDirector.BuildCreditSubject(
            "AlgorithmsLight",
            10,
            30,
            labWorksCollectionOne,
            lectureMaterialsCollectionOne);

        // Act
        BaseSubject subjectOnePrototype = subjectOne.CloneBase(userOne);
        BaseSubject subjectTwoPrototype = subjectTwo.CloneBase(userOne);

        // Assert
        Assert.Equal(subjectOne.Id, subjectOnePrototype.IdOfBasisSubject);
        Assert.Equal(subjectTwo.Id, subjectTwoPrototype.IdOfBasisSubject);
    }
}