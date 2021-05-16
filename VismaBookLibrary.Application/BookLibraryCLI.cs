using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Enums;
using VismaBookLibrary.Domain.Extensions;
using VismaBookLibrary.Domain.Factories;
using VismaBookLibrary.Domain.Interfaces;

namespace VismaBookLibrary.Application
{
    public class BookLibraryCLI
    {
        private readonly IWriter _writer;
        private readonly IPrintService _printService;
        private readonly CommandFactory _commandFactory;

        public BookLibraryCLI(IWriter writer, IPrintService printService, CommandFactory commandFactory)
        {
            _writer = writer;
            _printService = printService;
            _commandFactory = commandFactory;
        }

        public void ReadAndExecuteCommand()
        {
            _printService.PrintFromEnum<CommandEnums>();

            try
            {
                var commandString = _writer.ReadLine("\nPlease enter your command:");

                var command = _commandFactory.Build(commandString);

                command.Execute();
            }
            catch (ArgumentException ex)
            {
                _writer.PrintLine(ex.Message);
            }
        }
    }
}
