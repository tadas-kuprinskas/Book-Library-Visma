using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Helpers
{
    public static class MappingHelpers
    {
        public static Book MapToTakenBook(Book book, string customerName, DateTime returnDate)
        {
            var takenBook = new Book()
            {
                Name = book.Name,
                Author = book.Author,
                Category = book.Category,
                Language = book.Language,
                PublicationDate = book.PublicationDate,
                ISBN = book.ISBN,
                TakenBy = customerName,
                TakenOn = DateTime.Now,
                EstimatedReturn = returnDate
            };

            return takenBook;                          
        }

        public static Book MapToAvailableBook(Book takenBook)
        {
            var book = new Book()
            {
                Name = takenBook.Name,
                Author = takenBook.Author,
                Category = takenBook.Category,
                Language = takenBook.Language,
                PublicationDate = takenBook.PublicationDate,
                ISBN = takenBook.ISBN,
                EstimatedReturn = null,
                TakenBy = null,
                TakenOn = null
            };

            return book;
        }
    }
}
