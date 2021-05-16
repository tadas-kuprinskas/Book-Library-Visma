using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Models;
using VismaBookLibrary.Domain.Services;
using Xunit;

namespace VismaBookLibrary.UnitTests.Services
{
    public class JsonFileServiceTests
    {
        [Fact]
        public void SaveNew_GivenCorrectParameters_ThrownsNoExceptions()
        {
            var fixture = new Fixture();
            var book = fixture.Create<Book>();
            var jsonFileService = new JsonFileService();

            var exception = Record.Exception(() => jsonFileService.SaveNew(book));

            Assert.Null(exception);
        }

        [Fact]
        public void GetAll_GivenCorrectUrl_ReturnsNotEmptyBookList()
        {
            var jsonFileService = new JsonFileService();

            var books = jsonFileService.GetAll();
            books.Should().NotBeEmpty();
            books.Should().BeOfType<List<Book>>();
        }

        [Fact]
        public void Owerwrite_GivenCorrectParameters_ThrownsNoExceptions()
        {
            var fixture = new Fixture();
            var books = fixture.Create<IEnumerable<Book>>();
            var jsonFileService = new JsonFileService();

            var exception = Record.Exception(() => jsonFileService.Overwrite(books));

            Assert.Null(exception);
        }
    }
}
