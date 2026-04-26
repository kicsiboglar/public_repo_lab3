using DatesAndStuff.Interfaces;

namespace DatesAndStuff.Tests.Helpers;

internal class TestPaymentService : IPaymentService
{
    uint startCallCount = 0;
    uint specifyCallCount = 0;
    uint confirmCallCount = 0;

    /// <summary>
    /// Starts the payment process. This method should be called exactly once, and it should be the first method called in the payment process.
    /// If this method is called more than once, or if any other method is called before this method, an exception will be thrown.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void StartPayment()
    {
        if (startCallCount != 0 || specifyCallCount > 0 || confirmCallCount > 0)
            throw new Exception();

        startCallCount++;
    }

    /// <summary>
    /// Specifies the amount to be paid. This method should be called exactly once, and it should be called after the StartPayment method and before the ConfirmPayment method.
    /// </summary>
    /// <param name="amount"></param>
    /// <exception cref="Exception"></exception>
    public void SpecifyAmount(double amount)
    {
        if (startCallCount != 1 || specifyCallCount > 0 || confirmCallCount > 0)
            throw new Exception();

        specifyCallCount++;
    }

    /// <summary>
    /// Confirms the payment. This method should be called exactly once, and it should be called after the StartPayment method and the SpecifyAmount method.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void ConfirmPayment()
    {
        if (startCallCount != 1 || specifyCallCount != 1 || confirmCallCount > 0)
            throw new Exception();

        confirmCallCount++;
    }

    /// <summary>
    /// Determines whether all required steps have been completed successfully.
    /// </summary>
    /// <returns>true if the start, specify, and confirm steps have each been called exactly once; otherwise, false.</returns>
    public bool Successful()
    {
        return startCallCount == 1 && specifyCallCount == 1 && confirmCallCount == 1;
    }
}
