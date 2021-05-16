using System;
using System.Collections.Generic;
using System.Reflection;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Interfaces
{
    public interface IValidationService
    {
        Book ValidateBookToReturn(Book bookToReturn);
        string ValidateReturnDate(Book book, DateTime returnDate);
        void ValidateUniqueISBN(IEnumerable<Book> existingBooks, PropertyInfo property, Book book, string inputISBN);
        void ValidateFilterOption(IEnumerable<Book> filteredAvailable, IEnumerable<Book> filteredTaken, IEnumerable<Book> filteredAll, string option);
        void ValidateTakingBook(Book book, DateTime returnDate, List<Book> customerBooks, string bookISBN);
        void ValidateBookToDelete(string bookISBN, IEnumerable<Book> books);
        void ValidateIfEmptyList(IEnumerable<Book> books);
    }
}