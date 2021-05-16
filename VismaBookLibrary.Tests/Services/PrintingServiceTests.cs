using AutoFixture;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;
using VismaBookLibrary.Domain.Services;
using Xunit;
using VismaBookLibrary.Domain.Enums;

namespace VismaBookLibrary.UnitTests.Services
{
    public class PrintingServiceTests
    {
        [Fact]
        public void PrintFromList_GivenList_NoExceptionThrown()
        {
            var fixture = new Fixture();

            var listOfBooks = fixture.Create<List<Book>>();

            var mockWriter = new Mock<IWriter>();
            var printService = new PrintingService(mockWriter.Object);

            var exception = Record.Exception(() => printService.PrintFromList(listOfBooks));

            Assert.Null(exception);
        }

        [Fact]
        public void PrintFromEnum_GivenEnumType_NoExceptionThrown()
        {
            var mockWriter = new Mock<IWriter>();
            var printService = new PrintingService(mockWriter.Object);

            var exception = Record.Exception(() => printService.PrintFromEnum<CommandEnums>());

            Assert.Null(exception);
        }

        [Fact]
        public void PrintFromEnum_GivenWrongType_ExceptionThrown()
        {
            var mockWriter = new Mock<IWriter>();
            var printService = new PrintingService(mockWriter.Object);

            Assert.Throws<ArgumentException>(() => printService.PrintFromEnum<Book>());
        }
    }
}
