using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Enums;
using VismaBookLibrary.Domain.Factories;
using VismaBookLibrary.Domain.Interfaces;

namespace VismaBookLibrary.Domain.Commands
{
    public class FilterBooksCommand : ICommand
    {
        private readonly IWriter _writer;
        private readonly IPrintService _printService;
        private readonly FilterCommandFactory _filterCommandFactory;

        public FilterBooksCommand(IWriter writer, IPrintService printService, FilterCommandFactory filterCommandFactory)
        {
            _writer = writer;
            _printService = printService;
            _filterCommandFactory = filterCommandFactory;
        }

        public void Execute()
        {
            try
            {
                _printService.PrintFromEnum<FilteringEnums>();

                var commandString = _writer.ReadLine("\nPlease enter your command to filter the books:");

                var command = _filterCommandFactory.Build(commandString);

                var phrase = _writer.ReadLine("\nPlease enter your filter phrase:");

                var filterOption = _writer.ReadLine("\nFilter Options:\nTo filter available books enter: Available\nTo filter taken books enter: Taken" +
                    "\nTo filter all books enter: All\nPlease enter your filter option:");

                command.Execute(filterOption, phrase);
            }
            catch (ArgumentException ex)
            {
                _writer.PrintLine(ex.Message);
            }       
        }
    }
}
