using Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms.EducationalProgramsRepository;
using Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms.EducationalProgramTypes;
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

public class UpdateEntity_ShouldReturnUnauthorizedAccessException_WhenAttemptingToChangeByNotTheAuthor
{
    [Fact]
    public void LabWork_CheckUpdateAccess()
    {
        // Arrange
        var users = new UserRepository();
        var labWorks = new LabWorkRepository();

        var userOne = new User("Aboba", users);
        var userTwo = new User("Alex", users);

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
        labWorkOne.UpdateLabWork(userOne, newDescription: "labydi labydai ali babai");

        // Assert
        UnauthorizedAccessException exception = Assert.Throws<UnauthorizedAccessException>(() => labWorkOne.UpdateLabWork(userTwo, newLabWorkName: "Op"));
        Assert.Equal("Only the author can update this lab work.", exception.Message);
    }

    [Fact]
    public void LectureMaterial_CheckUpdateAccess()
    {
        // Arrange
        var users = new UserRepository();
        var lectureMaterials = new LecMaterialsRepository();

        var userOne = new User("Aboba", users);
        var userTwo = new User("Alex", users);

        var lectureMaterialOneDirector = new LectureMaterialDirector(userOne, lectureMaterials);
        LectureMaterial lectureMaterialOne = lectureMaterialOneDirector.BuildLectureMaterial(
            "Opa",
            "labydi labydai",
            "mega super content");

        // Act
        lectureMaterialOne.UpdateLectureMaterial(userOne, newShortDescription: "labydi labydai ali babai");

        // Assert
        UnauthorizedAccessException exception = Assert.Throws<UnauthorizedAccessException>(() => lectureMaterialOne.UpdateLectureMaterial(userTwo, newLectureMaterialName: "Op"));
        Assert.Equal("Only the author can update this lecture material.", exception.Message);
    }

    [Fact]
    public void Subject_CheckUpdateAccess()
    {
        // Arrange
        var users = new UserRepository();
        var subjects = new SubjectRepository();
        var labWorks = new LabWorkRepository();
        var lectureMaterials = new LecMaterialsRepository();

        var userOne = new User("Aboba", users);
        var userTwo = new User("Alex", users);

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

        var labWorksCollectionTwo = new Collection<LabWork>
        {
            labWorkOneDirector.BuildLabWork(
                "PisyatDva",
                "ono samoe",
                new Collection<EvaluationCriterion>
                {
                    new EvaluationCriterion { CriterionName = "Красивые глазки", CriterionMaxPoints = 15 },
                    new EvaluationCriterion { CriterionName = "Грамматика", CriterionMaxPoints = 10 },
                }),
        };

        var labWorksCollectionThree = new Collection<LabWork>
        {
            labWorkOneDirector.BuildLabWork(
                "Shytka",
                "minutka",
                new Collection<EvaluationCriterion>
                {
                    new EvaluationCriterion { CriterionName = "Приятность", CriterionMaxPoints = 10 },
                    new EvaluationCriterion { CriterionName = "Грамотность", CriterionMaxPoints = 15 },
                }),
            labWorkOneDirector.BuildLabWork(
                "Lick",
                "inside view",
                new Collection<EvaluationCriterion>
                {
                    new EvaluationCriterion { CriterionName = "Корректность", CriterionMaxPoints = 20 },
                    new EvaluationCriterion { CriterionName = "Красота", CriterionMaxPoints = 25 },
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
            labWorksCollectionThree,
            lectureMaterialsCollectionOne);

        // Act
        subjectOne.UpdateSubject(userOne, newLabWorksToAdd: labWorksCollectionTwo, labWorksToRemove: [labWorksCollectionOne[0]]);
        subjectTwo.UpdateSubject(userOne, newLabWorksToAdd: labWorksCollectionTwo, labWorksToRemove: [labWorksCollectionThree[0]]);

        // Assert
        UnauthorizedAccessException examException = Assert.Throws<UnauthorizedAccessException>(() => subjectOne.UpdateSubject(userTwo, newSubjectName: "Algo"));
        Assert.Equal("Only the author can update this subject.", examException.Message);

        UnauthorizedAccessException creditException = Assert.Throws<UnauthorizedAccessException>(() => subjectTwo.UpdateSubject(userTwo, newSubjectName: "AlgoLight"));
        Assert.Equal("Only the author can update this subject.", creditException.Message);
    }

    [Fact]
    public void EducationalProgram_CheckUpdateAccess()
    {
        // Arrange
        var users = new UserRepository();
        var subjects = new SubjectRepository();
        var labWorks = new LabWorkRepository();
        var lectureMaterials = new LecMaterialsRepository();
        var educationalPrograms = new EducationalProgramRepository();

        var userOne = new User("Aboba", users);
        var userTwo = new User("Alex", users);

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

        var labWorksCollectionTwo = new Collection<LabWork>
        {
            labWorkOneDirector.BuildLabWork(
                "PisyatDva",
                "ono samoe",
                new Collection<EvaluationCriterion>
                {
                    new EvaluationCriterion { CriterionName = "Красивые глазки", CriterionMaxPoints = 30 },
                    new EvaluationCriterion { CriterionName = "Грамматика", CriterionMaxPoints = 15 },
                }),
            labWorkOneDirector.BuildLabWork(
                "PisyatDva",
                "ono samoe",
                new Collection<EvaluationCriterion>
                {
                    new EvaluationCriterion { CriterionName = "Пунктуальность", CriterionMaxPoints = 15 },
                    new EvaluationCriterion { CriterionName = "Пунктуация", CriterionMaxPoints = 20 },
                }),
        };

        var labWorksCollectionThree = new Collection<LabWork>
        {
            labWorkOneDirector.BuildLabWork(
                "xxxx",
                "tier",
                new Collection<EvaluationCriterion>
                {
                    new EvaluationCriterion { CriterionName = "Красота", CriterionMaxPoints = 20 },
                    new EvaluationCriterion { CriterionName = "Отступы", CriterionMaxPoints = 20 },
                }),
            labWorkOneDirector.BuildLabWork(
                "digits",
                "moose",
                new Collection<EvaluationCriterion>
                {
                    new EvaluationCriterion { CriterionName = "Локализация", CriterionMaxPoints = 10 },
                    new EvaluationCriterion { CriterionName = "Функции", CriterionMaxPoints = 30 },
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

        var lectureMaterialsCollectionTwo = new Collection<LectureMaterial>
        {
            lectureMaterialOneDirector.BuildLectureMaterial(
                "Sharik",
                "klop klap",
                "krytoi content"),
        };

        var lectureMaterialsCollectionThree = new Collection<LectureMaterial>
        {
            lectureMaterialOneDirector.BuildLectureMaterial(
                "Lupa",
                "lupa pupa",
                "ultra content"),
        };

        var subjectOneDirector = new SubjectDirector(userOne, subjects);
        var subjectCollectionOne = new Collection<Tuple<BaseSubject, int>>
        {
            Tuple.Create<BaseSubject, int>(
                subjectOneDirector.BuildExamSubject(
                "Algorithms", 30, labWorksCollectionOne, lectureMaterialsCollectionOne),
                1),
            Tuple.Create<BaseSubject, int>(
                subjectOneDirector.BuildCreditSubject(
                "SDIT", 10, 20, labWorksCollectionTwo, lectureMaterialsCollectionTwo),
                2),
        };

        var subjectCollectionTwo = new Collection<Tuple<BaseSubject, int>>
        {
            Tuple.Create<BaseSubject, int>(
                subjectOneDirector.BuildExamSubject(
                    "Math", 20, labWorksCollectionThree, lectureMaterialsCollectionThree),
                1),
        };

        var bachelorEducationalProgramOneDirector = new EducationalProgramDirector<BachelorEducationalProgram>(userOne, educationalPrograms);
        BachelorEducationalProgram bachelorEducationalProgramOne = bachelorEducationalProgramOneDirector.BuildEducationalProgram(
                "Software Engineering",
                subjectCollectionOne);

        // Act
        bachelorEducationalProgramOne.UpdateEducationalProgram(userOne, newSubjectsToAdd: subjectCollectionTwo);

        // Assert
        UnauthorizedAccessException exception = Assert.Throws<UnauthorizedAccessException>(() => bachelorEducationalProgramOne.UpdateEducationalProgram(userTwo, newCreditSubjectName: "Computer Science"));
        Assert.Equal("Only the ProgramManager can update this EducationalProgram.", exception.Message);
    }

    [Fact]
    public void User_CheckUpdateAccess()
    {
        // Arrange
        var users = new UserRepository();

        var userOne = new User("Aboba", users);
        var userTwo = new User("Alex", users);

        // Act
        userOne.UpdateUser(userOne, newUserName: "Abobik");

        // Assert
        UnauthorizedAccessException exception = Assert.Throws<UnauthorizedAccessException>(() => userOne.UpdateUser(userTwo, newUserName: "Lox"));
        Assert.Equal("Only the User himself can update his account details.", exception.Message);
    }
}