using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Extensions;
using VismaBookLibrary.Domain.Helpers;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Commands
{
    public class TakeBookCommand : ICommand
    {
        private readonly IWriter _writer;
        private readonly IFileService _fileService;
        private readonly IValidationService _validationService;

        public TakeBookCommand(IWriter writer, IFileService fileService, IValidationService validationService)
        {
            _writer = writer;
            _fileService = fileService;
            _validationService = validationService;
        }

        public void Execute()
        {
            var allBooks = _fileService.GetAll();
            var book = GetTakenBook();

            RemoveFromList(allBooks, book);
            _fileService.SaveNew(book);
        }

        public Book GetTakenBook()
        {
            var customerName = _writer.ReadLine("Please enter your name");
            var bookISBN = _writer.ReadLine("Please enter the ISBN code of the book you want to take");
            var returnDate = _writer.ReadLine("Please enter estimated return date (year-month-day)").ParseStringToDate();

            var customerBooks = _fileService.GetAll().Where(b => b.TakenBy == customerName).ToList();
            var book = _fileService.GetAll().FirstOrDefault(b => b.ISBN == bookISBN);

            _validationService.ValidateTakingBook(book, returnDate, customerBooks, bookISBN);

            return MappingHelpers.MapToTakenBook(book, customerName, returnDate);
        }

        public void RemoveFromList(IEnumerable<Book> books, Book book)
        {
            var filteredBooks = books.Where(b => b.ISBN != book.ISBN);
            _fileService.Overwrite(filteredBooks);
        }
    }
}
