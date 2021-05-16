using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Commands;
using VismaBookLibrary.Domain.Factories;
using VismaBookLibrary.Domain.Interfaces;
using Xunit;

namespace VismaBookLibrary.UnitTests.Factories
{
    public class CommandFactoryTests
    {
        [Fact]
        public void Build_GivenCorrectInput_ReturnsCorrectCommand()
        {
            var mockFileService = new Mock<IFileService>();
            var mockValidationService = new Mock<IValidationService>();
            var mockIWriter = new Mock<IWriter>();
            var mockPrintService = new Mock<IPrintService>();

            var filterCommandFactory = new FilterCommandFactory(mockFileService.Object, mockValidationService.Object);

            var input = "add";

            var commandFactory = new CommandFactory(mockIWriter.Object, mockFileService.Object, mockPrintService.Object, 
                filterCommandFactory, mockValidationService.Object);

            var command = commandFactory.Build(input);

            command.Should().BeOfType<AddBookCommand>();
        }
    }
}
