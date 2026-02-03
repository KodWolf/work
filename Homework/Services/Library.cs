using System;
using System.Collections.Generic;

class Library
{
    private List<Book> books = new List<Book>();
    private List<Reader> readers = new List<Reader>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine("Книга добавлена: " + book.Title);
    }

    public void RemoveBook(string isbn)
    {
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].ISBN == isbn)
            {
                Console.WriteLine("Книга удалена: " + books[i].Title);
                books.RemoveAt(i);
                return;
            }
        }
        Console.WriteLine("Книга не найдена");
    }

    public void RegisterReader(Reader reader)
    {
        readers.Add(reader);
        Console.WriteLine("Читатель зарегистрирован: " + reader.Name);
    }

    public void RemoveReader(int readerId)
    {
        for (int i = 0; i < readers.Count; i++)
        {
            if (readers[i].ReaderId == readerId)
            {
                Console.WriteLine("Читатель удалён: " + readers[i].Name);
                readers.RemoveAt(i);
                return;
            }
        }
        Console.WriteLine("Читатель не найден");
    }

    public void IssueBook(string isbn)
    {
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].ISBN == isbn)
            {
                if (books[i].Copies > 0)
                {
                    books[i].Copies--;
                    Console.WriteLine("Книга выдана: " + books[i].Title);
                }
                else
                {
                    Console.WriteLine("Нет доступных экземпляров");
                }
                return;
            }
        }
        Console.WriteLine("Книга не найдена");
    }

    public void ReturnBook(string isbn)
    {
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].ISBN == isbn)
            {
                books[i].Copies++;
                Console.WriteLine("Книга возвращена: " + books[i].Title);
                return;
            }
        }
        Console.WriteLine("Книга не найдена");
    }
}
