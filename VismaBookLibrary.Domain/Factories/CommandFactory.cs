using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Commands;
using VismaBookLibrary.Domain.Commands.FilteringCommands;
using VismaBookLibrary.Domain.Enums;
using VismaBookLibrary.Domain.Extensions;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Services;

namespace VismaBookLibrary.Domain.Factories
{
    public class CommandFactory
    {
        private readonly IWriter _writer;
        private readonly IFileService _fileService;
        private readonly IPrintService _printService;
        private readonly FilterCommandFactory _filterCommandFactory;
        private readonly IValidationService _validationService;

        public CommandFactory(IWriter writer, IFileService fileService, IPrintService printService, FilterCommandFactory filterCommandFactory, 
            IValidationService validationService)
        {
            _writer = writer;
            _fileService = fileService;
            _printService = printService;
            _filterCommandFactory = filterCommandFactory;
            _validationService = validationService;
        }

        public ICommand Build(string input)
        {            
            if (Enum.TryParse(input.FirstLetterToUpper(), out CommandEnums commandEnum))
            {
                switch (commandEnum)
                {
                    case CommandEnums.Add:
                        return new AddBookCommand(_writer, _fileService, _validationService);
                    case CommandEnums.Delete:
                        return new DeleteBookCommand(_writer, _fileService, _validationService);
                    case CommandEnums.Take:
                        return new TakeBookCommand(_writer, _fileService, _validationService);
                    case CommandEnums.Return:
                        return new ReturnBookCommand(_writer, _fileService, _validationService);
                    case CommandEnums.All:
                        return new ListBooksCommand(_fileService, _printService, _validationService);
                    case CommandEnums.Filter:
                        return new FilterBooksCommand(_writer, _printService, _filterCommandFactory);
                }
            }
            throw new ArgumentException("\nCommand was not recognised");
        }
    }
}
