class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Выберите тип документа:");
        Console.WriteLine("1 - Report");
        Console.WriteLine("2 - Resume");
        Console.WriteLine("3 - Letter");
        Console.WriteLine("4 - Invoice");

        string choice = Console.ReadLine();

        DocumentCreator creator = null;

        if (choice == "1")
        {
            creator = new ReportCreator();
        }
        else if (choice == "2")
        {
            creator = new ResumeCreator();
        }
        else if (choice == "3")
        {
            creator = new LetterCreator();
        }
        else if (choice == "4")
        {
            creator = new InvoiceCreator();
        }
        else
        {
            Console.WriteLine("Неверный выбор.");
            return;
        }

        Document document = creator.CreateDocument();
        document.Open();
    }
}
