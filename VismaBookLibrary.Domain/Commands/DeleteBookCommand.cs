using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Commands
{
    public class DeleteBookCommand : ICommand
    {
        private readonly IWriter _writer;
        private readonly IFileService _fileService;
        private readonly IValidationService _validationService;

        public DeleteBookCommand(IWriter writer, IFileService fileService, IValidationService validationService)
        {
            _writer = writer;
            _fileService = fileService;
            _validationService = validationService;
        }

        public void Execute()
        {
            var bookISBN = _writer.ReadLine("Please enter the ISBN code of the book");

            var books = _fileService.GetAll();
            
            var filteredBooks = books.Where(b => b.ISBN != bookISBN);

            _validationService.ValidateBookToDelete(bookISBN, books);

            _fileService.Overwrite(filteredBooks);           
        }
    }
}
