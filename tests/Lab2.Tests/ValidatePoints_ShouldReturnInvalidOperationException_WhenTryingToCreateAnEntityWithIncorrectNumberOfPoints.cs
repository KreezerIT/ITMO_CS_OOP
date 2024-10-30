using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks;
using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.EvaluationCriteria;
using Itmo.ObjectOrientedProgramming.Lab2.LaboratoryWorks.LaboratoryWorksRepository;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterials.LectureMaterialsRepository;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.SubjectsRepository;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using Itmo.ObjectOrientedProgramming.Lab2.Users.UsersRepository;
using System.Collections.ObjectModel;
using Xunit;

namespace Lab2.Tests;

public class ValidatePoints_ShouldReturnInvalidOperationException_WhenTryingToCreateAnEntityWithIncorrectNumberOfPoints
{
    [Fact]
    public void Subject_CheckPointsValidation()
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
                    new EvaluationCriterion { CriterionName = "Жесткость", CriterionMaxPoints = 20 - 5 },
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

        // Act and Assert
        InvalidOperationException examException = Assert.Throws<InvalidOperationException>(() =>
            subjectOneDirector.BuildExamSubject(
                "Algorithms",
                30,
                labWorksCollectionOne,
                lectureMaterialsCollectionOne));
        Assert.Equal("TotalExamSubjectPoints must equal 100.", examException.Message);

        InvalidOperationException creditException = Assert.Throws<InvalidOperationException>(() =>
            subjectOneDirector.BuildCreditSubject(
                "AlgorithmsLight",
                10,
                30,
                labWorksCollectionOne,
                lectureMaterialsCollectionOne));
        Assert.Equal("TotalCreditSubjectPoints must equal 100.", creditException.Message);
    }
}