using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Commands
{
    public class ListBooksCommand : ICommand
    {
        private readonly IFileService _fileService;
        private readonly IPrintService _printService;
        private readonly IValidationService _validationService;

        public ListBooksCommand(IFileService fileService, IPrintService printService, IValidationService validationService)
        {
            _fileService = fileService;
            _printService = printService;
            _validationService = validationService;
        }

        public void Execute()
        {
            ListAllBooks();
        }

        public void ListAllBooks()
        {
            var allAvailableBooks = _fileService.GetAll();

            _validationService.ValidateIfEmptyList(allAvailableBooks);

            _printService.PrintFromList(allAvailableBooks);
        }       
    }
}
