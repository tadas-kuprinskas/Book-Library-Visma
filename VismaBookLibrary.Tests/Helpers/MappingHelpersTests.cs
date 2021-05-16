using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Helpers;
using VismaBookLibrary.Domain.Models;
using Xunit;

namespace VismaBookLibrary.UnitTests.Helpers
{
    public class MappingHelpersTests
    {
        [Fact]
        public void MapToTakenBook_GivenCustomerName_ReturnsTakenBook()
        {
            var fixture = new Fixture();

            fixture.Customize<Book>(b => b.With(p => p.TakenBy, ""));
            var book = fixture.Create<Book>();

            var customerName = "Joe";
            var returnDate = DateTime.Now.AddMonths(1);

            var takenBook = MappingHelpers.MapToTakenBook(book, customerName, returnDate);

            takenBook.TakenBy.Should().Be("Joe");
        }

        [Fact]
        public void MapToAvailableBook_GivenTakenBook_ReturnsAvailableBook()
        {
            var fixture = new Fixture();

            fixture.Customize<Book>(b => b.With(p => p.TakenBy, "Joe"));
            var takenBook = fixture.Create<Book>();

            var book = MappingHelpers.MapToAvailableBook(takenBook);

            book.TakenBy.Should().BeNullOrEmpty();
        }
    }
}
