using System;
using System.Collections.Generic;

namespace DependencyInversionPrinciple
{
    public class Student : IUser, IBorrower
    {
        public string GetUserType() => "Student";

        public void BorrowBook(string book)
        {
            Console.WriteLine($"{GetUserType()} borrowed the book: {book}");
        }
    }

    public class Teacher : IUser, IBorrower, IReserver
    {
        public string GetUserType() => "Teacher";

        public void BorrowBook(string book)
        {
            Console.WriteLine($"{GetUserType()} borrowed the book: {book}");
        }

        public void ReserveBook(string book)
        {
            Console.WriteLine($"{GetUserType()} reserved the book: {book}");
        }
    }

    public class Librarian : IUser, IInventoryManager
    {
        public string GetUserType() => "Librarian";

        public void AddBook(string book)
        {
            Console.WriteLine($"{GetUserType()} added the book: {book} to the inventory.");
        }

        public void RemoveBook(string book)
        {
            Console.WriteLine($"{GetUserType()} removed the book: {book} from the inventory.");
        }
    }
}
