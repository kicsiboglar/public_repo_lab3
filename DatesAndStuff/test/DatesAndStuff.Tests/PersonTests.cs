using AutoFixture;
using DatesAndStuff.Interfaces;
using DatesAndStuff.Models;
using DatesAndStuff.Tests.Helpers;
using FluentAssertions;

namespace DatesAndStuff.Tests;

public class PersonTests
{


    [Test]
    public void GotMarried_First_NameShouldChange()
    {
        // Arrange
        var sut = PersonFactory.CreateTestPerson();

        string newName = "Testini Testelina";
        double salaryBeforeMarriage = sut.Salary;
        var beforeChanges = Person.Clone(sut);

        // Act
        sut.GotMarried(newName);

        // Assert
        Assert.That(sut.Name, Is.EqualTo(newName)); // act = exp

        sut.Name
            .Should()
            .Be(newName);
        sut.Should().BeEquivalentTo(beforeChanges, o => o.Excluding(p => p.Name));
    }

    [Test]
    public void GotMarried_Second_ShouldFail()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Customize<IPaymentService>(c => c.FromFactory(() => new TestPaymentService()));

        var sut = fixture.Create<Person>();

        sut.GotMarried("Testelano Testelina");

        // Act
        var task = Task.Run(() => sut.GotMarried("Testerini Testelina"));
        try { task.Wait(); } catch { }

        // Assert
        Assert.IsTrue(task.IsFaulted);
    }

    [TestCase(10)]
    [TestCase(20)]
    [TestCase(0)]
    [TestCase(-5)]
    [TestCase(-9.99)]
    public void IncreaseSalary_WithValidPercentage_ShouldModifySalary(double salaryIncreasePercentage)
    {
        // Arrange
        Person sut = PersonFactory.CreateTestPerson();
        double initialSalary = sut.Salary;

        // Act
        sut.IncreaseSalary(salaryIncreasePercentage);

        // Assert
        sut.Salary.Should().BeApproximately(initialSalary * (100 + salaryIncreasePercentage) / 100, Math.Pow(10, -8), because: "numerical salary calculation might be rounded to conform legal stuff");
    }

    [TestCase(-100.01)]
    [TestCase(-10)]
    [TestCase(-20)]
    public void IncreaseSalary_WithInvalidPercentage_ShouldThrow(double salaryIncreasePercentage)
    {
        // Arrange
        Person sut = PersonFactory.CreateTestPerson();
        // Act
        var task = Task.Run(() => sut.IncreaseSalary(salaryIncreasePercentage));
        try { task.Wait(); } catch { }
        // Assert
        Assert.IsTrue(task.IsFaulted);
    }

    [Test]
    public void Constructor_DefaultParams_ShouldBeAbleToEatChocolate()
    {
        // Arrange

        // Act
        Person sut = PersonFactory.CreateTestPerson();

        // Assert
        sut.CanEatChocolate.Should().BeTrue();
    }

    [Test]
    public void Constructor_DontLikeChocolate_ShouldNotBeAbleToEatChocolate()
    {
        // Arrange

        // Act
        Person sut = PersonFactory.CreateTestPerson(fp => fp.CanEatChocolate = false);

        // Assert
        sut.CanEatChocolate.Should().BeFalse();
    }
}