using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Extensions;
using VismaBookLibrary.Domain.Enums;
using VismaBookLibrary.Domain.Commands.FilteringCommands;
using VismaBookLibrary.Domain.Services;

namespace VismaBookLibrary.Domain.Factories
{
    public class FilterCommandFactory
    {
        private readonly IFileService _fileService;
        private readonly IValidationService _validationService;

        public FilterCommandFactory(IFileService fileService, IValidationService validationService)
        {
            _fileService = fileService;
            _validationService = validationService;
        }

        public IFilterCommand Build(string input)
        {
            if (Enum.TryParse(input.FirstLetterToUpper(), out FilteringEnums commandEnum))
            {
                switch (commandEnum)
                {
                    case FilteringEnums.Isbn:
                        return new FilterByISBNCommand(_fileService, _validationService);
                    case FilteringEnums.Language:
                        return new FilterByLanguageCommand(_fileService, _validationService);
                    case FilteringEnums.Name:
                        return new FilterByNameCommand(_fileService, _validationService);
                    case FilteringEnums.Author:
                        return new FilterByAuthorCommand(_fileService, _validationService);
                    case FilteringEnums.Category:
                        return new FilterByCategoryCommand(_fileService, _validationService);                   
                }
            }

            throw new ArgumentException("\nFilter command was not recognised");
        }
    }
}
