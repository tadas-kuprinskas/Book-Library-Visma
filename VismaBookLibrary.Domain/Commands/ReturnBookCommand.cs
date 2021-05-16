using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Extensions;
using VismaBookLibrary.Domain.Helpers;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Commands
{
    public class ReturnBookCommand : ICommand
    {
        private readonly IWriter _writer;
        private readonly IFileService _fileService;
        private readonly IValidationService _validationService;

        public ReturnBookCommand(IWriter writer, IFileService fileService, IValidationService validationService)
        {
            _writer = writer;
            _fileService = fileService;
            _validationService = validationService;
        }

        public void Execute()
        {
            var bookToReturn = GetReturnedBook();
            var books = _fileService.GetAll();

            RemoveFromTaken(books, bookToReturn);
            _fileService.SaveNew(bookToReturn);
        }

        public Book GetReturnedBook()
        {
            var bookISBN = _writer.ReadLine("Please enter the ISBN code of the book you want to return");

            var returnDate = _writer.ReadLine("Please enter the date of returning the book (year-month-day)").ParseStringToDate();

            var bookToReturn = _fileService.GetAll().Where(b => b.TakenBy != null).FirstOrDefault(b => b.ISBN == bookISBN);

            _validationService.ValidateBookToReturn(bookToReturn);          

            _writer.PrintLine(_validationService.ValidateReturnDate(bookToReturn, returnDate));

            return MappingHelpers.MapToAvailableBook(bookToReturn);
        }

        public void RemoveFromTaken(IEnumerable<Book> books, Book book)
        {
            var filteredBooks = books.Where(b => b.ISBN != book.ISBN);
            _fileService.Overwrite(filteredBooks);
        }
    }
}
