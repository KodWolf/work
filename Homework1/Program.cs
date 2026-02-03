class Program
{
    static void Main()
    {
        Library library = new Library();

        Book book1 = new Book("The Pragmatic Programmer", "Andrew Hunt", "101", 4);
        Book book2 = new Book("Refactoring", "Martin Fowler", "202", 5);

        Reader reader1 = new Reader("Алекс", 1);
        Reader reader2 = new Reader("Лена", 2);

        library.AddBook(book1);
        library.AddBook(book2);

        library.RegisterReader(reader1);
        library.RegisterReader(reader2);

        library.IssueBook("101");
        library.IssueBook("101");
        library.ReturnBook("101");

        library.RemoveBook("202");
        library.RemoveReader(2);
    }
}

