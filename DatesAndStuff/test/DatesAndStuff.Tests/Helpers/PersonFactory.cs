using DatesAndStuff.Models;

namespace DatesAndStuff.Tests.Helpers;

/// <summary>
/// Creates Person objects for testing purposes. This is a simple factory class that allows you to create Person objects with default values,
/// and optionally modify the food preferences using a lambda function.
/// Use AutoFixture instead of this
/// This is an example from seminar to create a new person object without external packages
/// </summary>
internal class PersonFactory
{
    /// <summary>
    /// Creates a test person with default food preferences (can eat everything).
    /// </summary>
    /// <returns></returns>
    public static Person CreateTestPerson()
    {
        return CreateTestPerson(fp => { });
    }

    /// <summary>
    /// Creates a test person with the given food preferences. By default, the person can eat everything, but you can modify that by passing in a lambda that modifies the FoodPreferenceParams object.
    /// </summary>
    /// <param name="foodPreferenceModifyer"></param>
    /// <returns></returns>
    public static Person CreateTestPerson(Action<FoodPreferenceParams> foodPreferenceModifyer)
    {
        var fp = new FoodPreferenceParams()
        {
            CanEatChocolate = true,
            CanEatEgg = true,
            CanEatLactose = true,
            CanEatGluten = true
        };

        foodPreferenceModifyer(fp);

        return new Person("Testelano Testelina",
            new EmploymentInformation(
                18,
                new Employer("taxID", "Addressano", "Employero Employer", new List<int>() { 6201, 7210 })),
            new TestPaymentService(),
            new LocalTaxData("taxData"),
            fp
        );
    }
}
