using DatesAndStuff.Interfaces;
using DatesAndStuff.Models;
using DatesAndStuff.Tests.Helpers;
using FluentAssertions;
using Moq;

namespace DatesAndStuff.Tests;

internal class PaymentServiceTest
{
    /// <summary>
    /// Example of a manual mock implementation of IPaymentService for testing purposes
    /// </summary>
    [Test]
    public void TestPaymentService_ManualMock()
    {
        // Arrange
        Person sut = new Person("Testelini Testelina",
             new EmploymentInformation(
                 18,
                 new Employer("taxID", "Addressano", "Employero Employer", new List<int>() { 6201, 7210 })),
             new TestPaymentService(),
             new LocalTaxData("uat123"),
             new FoodPreferenceParams()
             {
                 CanEatChocolate = true,
                 CanEatEgg = true,
                 CanEatLactose = true,
                 CanEatGluten = true
             }
        );

        // Act
        bool result = sut.PerformSubsriptionPayment();

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Example of using Moq to create a mock implementation of IPaymentService for testing purposes
    /// </summary>
    [Test]
    public void TestPaymentService_Mock()
    {
        // Arrange
        // Mock setup with sequence to ensure the order of method calls
        var paymentSequence = new MockSequence();

        // Create the mock for IPaymentService
        var paymentService = new Mock<IPaymentService>();

        // Setup the expected calls in sequence
        paymentService.InSequence(paymentSequence).Setup(m => m.StartPayment());
        paymentService.InSequence(paymentSequence).Setup(m => m.SpecifyAmount(Person.SubscriptionFee));
        paymentService.InSequence(paymentSequence).Setup(m => m.ConfirmPayment());

        var paymentServiceMock = paymentService.Object;

        Person sut = new Person("Testelini Testelina",
             new EmploymentInformation(
                 54,
                 new Employer("taxID", "Addressano", "Employero Employer", new List<int>() { 6201, 7210 })),
             paymentServiceMock,
             new LocalTaxData("uat123"),
             new FoodPreferenceParams()
             {
                 CanEatChocolate = true,
                 CanEatEgg = true,
                 CanEatLactose = true,
                 CanEatGluten = true
             }
        );

        // Act
        bool result = sut.PerformSubsriptionPayment();

        // Assert
        result.Should().BeTrue();
        paymentService.Verify(m => m.StartPayment(), Times.Once);
        paymentService.Verify(m => m.SpecifyAmount(Person.SubscriptionFee), Times.Once);
        paymentService.Verify(m => m.ConfirmPayment(), Times.Once);
    }

    /// <summary>
    /// Example of using a custom AutoData attribute to automatically generate test data and mocks for testing purposes
    /// </summary>
    /// <param name="sut"></param>
    /// <param name="paymentService"></param>
    [Test]
    [CustomPersonCreationAutodataAttribute]
    public void TestPaymentService_MockWithAutodata(Person sut, Mock<IPaymentService> paymentService)
    {
        // Arrange

        // Act
        bool result = sut.PerformSubsriptionPayment();

        // Assert
        result.Should().BeTrue();
        paymentService.Verify(m => m.StartPayment(), Times.Once);
        paymentService.Verify(m => m.SpecifyAmount(Person.SubscriptionFee), Times.Once);
        paymentService.Verify(m => m.ConfirmPayment(), Times.Once);
    }
}
