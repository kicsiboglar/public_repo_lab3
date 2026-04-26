using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using DatesAndStuff.Interfaces;
using DatesAndStuff.Models;
using Moq;

namespace DatesAndStuff.Tests.Helpers;

/// <summary>
/// Provides an AutoDataAttribute for unit tests that generates test data using a customized fixture, including mocked
/// payment service interactions and a specific double value range.
/// </summary>
/// <remarks>This attribute is intended for use with test methods to supply automatically generated data. The
/// fixture is customized to inject a mocked IPaymentService with a predefined call sequence and to generate double
/// values within the range -11 to 20. This setup facilitates testing scenarios that require specific payment service
/// behavior and controlled numeric data.</remarks>
internal class CustomPersonCreationAutodataAttribute : AutoDataAttribute
{
    public CustomPersonCreationAutodataAttribute()
    : base(() =>
    {
        var fixture = new Fixture();

        fixture.Customize(new AutoMoqCustomization());

        var paymentSequence = new MockSequence();
        var paymentService = new Mock<IPaymentService>();



        paymentService.InSequence(paymentSequence).Setup(m => m.StartPayment());
        paymentService.InSequence(paymentSequence).Setup(m => m.SpecifyAmount(Person.SubscriptionFee));
        paymentService.InSequence(paymentSequence).Setup(m => m.ConfirmPayment());

        fixture.Inject(paymentService);

        //fixture.Register<IPaymentService>(() => new TestPaymentService());

        double top = 20;
        double bottom = -11;
        fixture.Customize<double>(c => c.FromFactory(() => new Random().NextDouble() * (top - (bottom)) + bottom));
        return fixture;
    })
    { }
}
