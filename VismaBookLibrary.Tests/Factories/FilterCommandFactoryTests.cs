using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Commands.FilteringCommands;
using VismaBookLibrary.Domain.Factories;
using VismaBookLibrary.Domain.Interfaces;
using Xunit;

namespace VismaBookLibrary.UnitTests.Factories
{
    public class FilterCommandFactoryTests
    {
        [Fact]
        public void Build_GivenCorrectInput_ReturnsCorrectCommand()
        {
            var mockFileService = new Mock<IFileService>();
            var mockValidationService = new Mock<IValidationService>();

            var input = "Language";

            var filterCommandFactory = new FilterCommandFactory(mockFileService.Object, mockValidationService.Object);

            var command = filterCommandFactory.Build(input);

            command.Should().BeOfType<FilterByLanguageCommand>();
        }
    }
}
