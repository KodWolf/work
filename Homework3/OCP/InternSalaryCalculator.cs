public class InternSalaryCalculator : ISalaryCalculator
{
    public double Calculate(Employee employee)
    {
        return employee.BaseSalary * 0.8;
    }
}
