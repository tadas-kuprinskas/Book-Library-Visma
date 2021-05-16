using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IWriter _writer;
        private readonly IFileService _fileService;
        private readonly IValidationService _validationService;

        public AddBookCommand(IWriter writer, IFileService fileService, IValidationService validationService)
        {
            _writer = writer;
            _fileService = fileService;
            _validationService = validationService;
        }

        public void Execute()
        {
            var existingBooks = _fileService.GetAll();

            _fileService.SaveNew(ReadInputToModel(existingBooks));
        }

        public Book ReadInputToModel(IEnumerable<Book> existingBooks)
        {
            var book = new Book();
            PropertyInfo[] properties = typeof(Book).GetProperties();            

            foreach (var property in properties)
            {
                if(property.Name == "TakenBy")
                {
                    break;
                }

                var value = _writer.ReadLine($"Please enter value for {property.Name}");               

                _validationService.ValidateUniqueISBN(existingBooks, property, book, value);
            }

            return book;
        }
    }
}
