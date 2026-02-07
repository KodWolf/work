public class ContractSalaryCalculator : ISalaryCalculator
{
    public double Calculate(Employee employee)
    {
        return employee.BaseSalary * 1.1;
    }
}
