namespace DatesAndStuff.Interfaces;

public interface IPaymentService
{
    public void StartPayment();

    public void SpecifyAmount(double amount);

    public void ConfirmPayment();

    public bool Successful();
}
