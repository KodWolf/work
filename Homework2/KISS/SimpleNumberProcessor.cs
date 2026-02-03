namespace DesignPrinciplesDemo.KISS
{
    public class SimpleNumberProcessor
    {
        public void ProcessNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return;

            foreach (var number in numbers)
            {
                if (number > 0)
                {
                    Console.WriteLine(number);
                }
            }
        }

        public void PrintPositiveNumbers(int[] numbers)
        {
            foreach (var number in numbers)
            {
                if (number > 0)
                {
                    Console.WriteLine(number);
                }
            }
        }

        public int Divide(int a, int b)
        {
            if (b == 0)
            {
                Console.WriteLine("Ошибка: деление на ноль");
                return 0;
            }

            return a / b;
        }
    }
}