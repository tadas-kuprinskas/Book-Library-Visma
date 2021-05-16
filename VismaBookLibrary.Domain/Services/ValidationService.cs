using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Services
{
    public class ValidationService : IValidationService
    {       
        private readonly IPrintService _printService;

        public ValidationService(IPrintService printService)
        {                       
            _printService = printService;
        }
        public string ValidateReturnDate(Book book, DateTime returnDate)
        {
            if(book.EstimatedReturn < returnDate)
            {
                return "He who is in a hurry always arrives late. Please read slowly but return books on time:)";
            }
            else if(returnDate < DateTime.Now)
            {
                return "Book was returned earlier. We probably missed it";
            }
            else
            {
                return "Thank you for returning the book on time";
            }
        }

        public void ValidateUniqueISBN(IEnumerable<Book> existingBooks,
            PropertyInfo property, Book book, string inputValue)
        {
            var existingBook = existingBooks.FirstOrDefault(b => b.ISBN == inputValue);
            
            if(property.Name == "ISBN")
            {
                if (existingBook == null)
                {
                    property.SetValue(book, inputValue);
                }
                else
                {
                    throw new ArgumentException("ISBN is a unique value. Check your ISBN input");
                }
            }
            else
            {
                property.SetValue(book, inputValue);
            }
            
        }

        public Book ValidateBookToReturn(Book bookToReturn)
        {
            if (bookToReturn is null)
            {
                throw new ArgumentException("\nThe book you want to return doesn't exist in our Library");
            }
            else
            {
                return bookToReturn;
            }
        }

        public void ValidateFilterOption(IEnumerable<Book> filteredAvailable, IEnumerable<Book> filteredTaken, IEnumerable<Book> filteredAll, string option)
        {
            if (option.ToLower() == "taken")
            {               
                _printService.PrintFromList(filteredTaken);
            }
            else if (option.ToLower() == "available")
            {             
                _printService.PrintFromList(filteredAvailable);
            }
            else if (option.ToLower() == "all")
            {
                _printService.PrintFromList(filteredAll);
            }
            else
            {
                throw new ArgumentException("Please check your filter option");
            }
        }

        public void ValidateTakingBook(Book book, DateTime returnDate, List<Book> customerBooks, string bookISBN)
        {
            if (customerBooks.Count >= 3)
            {
                throw new ArgumentException("\nYou can't take more than 3 books");
            }
            else if (book == null)
            {
                throw new ArgumentException("\nSuch book doesn't exist or the book is unavailable");
            }
            else if (DateTime.Now.AddMonths(2).CompareTo(returnDate) < 0)
            {
                throw new ArgumentException("\nBook can be taken for not longer than 2 months");
            }
            else if (returnDate < DateTime.Now)
            {
                throw new ArgumentException("\nCheck your return date");
            }
            else if (book.TakenBy != null)
            {
                throw new ArgumentException("\nThis book is already taken");
            }
        }

        public void ValidateBookToDelete(string bookISBN, IEnumerable<Book> books)
        {
            bool ifExists = books.Any(b => b.ISBN == bookISBN);

            if (!ifExists)
            {
                throw new ArgumentException("\nSuch book doesn't exist");
            }
        }

        public void ValidateIfEmptyList(IEnumerable<Book> books)
        {
            if(books.ToList().Count == 0)
            {
                throw new ArgumentException("\nOur library is empty at the moment. We need to add new books");
            }
        }
       
    }
}
