namespace DatesAndStuff.Models;

public class EmploymentInformation
{
    public double Salary { get; private set; }

    public Employer Employer { get; private set; }

    public EmploymentInformation(double salary, Employer e)
    {
        this.Salary = salary;
        this.Employer = e;
    }

    /// <summary>
    /// Increase the salary by a percentage. The percentage can be negative, but not less than -10% (i.e. the salary cannot be reduced by more than 10%).
    /// </summary>
    /// <param name="percentage"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void IncreaseSalary(double percentage)
    {
        if (percentage <= -10)
            throw new ArgumentOutOfRangeException(nameof(percentage));

        this.Salary = this.Salary * (1 + percentage / 100);
    }
}
