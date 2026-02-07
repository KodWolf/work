public class PermanentSalaryCalculator : ISalaryCalculator
{
    public double Calculate(Employee employee)
    {
        return employee.BaseSalary * 1.2;
    }
}
