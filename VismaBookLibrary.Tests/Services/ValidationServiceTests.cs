using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Application.Writers;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;
using VismaBookLibrary.Domain.Services;
using Xunit;

namespace VismaBookLibrary.UnitTests.Services
{
    public class ValidationServiceTests
    {
        [Fact]
        public void ValidateReturnDate_GivenCorrectParameters_ReturnsTheRightMessage()
        {            
            var fixture = new Fixture();
            
            fixture.Customize<Book>(b => b.With(p => p.EstimatedReturn, DateTime.Now));
            var book = fixture.Create<Book>();

            var returnDate = DateTime.Now.AddMonths(4);

            var mockPrintService = new Mock<IPrintService>();
            var validationService = new ValidationService(mockPrintService.Object);

            var textResult = validationService.ValidateReturnDate(book, returnDate);

            textResult.Should().Contain("He who is in a hurry");
        }

        [Fact]
        public void ValidateUniqueISBN_GivenExistingISBN_ThrowsArgumentException()
        {
            var fixture = new Fixture();

            fixture.Customize<Book>(b => b.With(p => p.ISBN, "678w"));
            var bookOne = fixture.Create<Book>();

            fixture.Customize<Book>(b => b.With(p => p.ISBN, "823a"));
            var bookTwo = fixture.Create<Book>();          

            List<Book> books = new() { bookOne, bookTwo };

            var property = typeof(Book).GetProperties().FirstOrDefault(p => p.Name == "ISBN");

            string inputISBN = "678w";

            var mockPrintService = new Mock<IPrintService>();
            var validationService = new ValidationService(mockPrintService.Object);

            Assert.Throws<ArgumentException>(() => validationService.ValidateUniqueISBN(books, property, bookTwo, inputISBN))
                .Message.Should().Contain("ISBN is a unique value");
        }

        [Fact]
        public void ValidateBookToReturn_GivenTakenBook_ReturnsBook()
        {
            var fixture = new Fixture();

            var book = fixture.Create<Book>();

            var mockPrintService = new Mock<IPrintService>();
            var validationService = new ValidationService(mockPrintService.Object);

            var bookToReturn = validationService.ValidateBookToReturn(book);

            bookToReturn.Should().NotBeNull();
            bookToReturn.Should().BeOfType<Book>();
        }

        [Fact]
        public void ValidateFilterOption_GivenIncorrectInput_ThrowsArgumentException()
        {
            var fixture = new Fixture();

            fixture.Customize<Book>(b => b.With(p => p.TakenBy, ""));
            var firstAvailableBook = fixture.Create<Book>();
            var secondAvailableBook = fixture.Create<Book>();

            fixture.Customize<Book>(b => b.With(p => p.TakenBy, "name"));
            var firstTakenBook = fixture.Create<Book>();
            var secondTakenBook = fixture.Create<Book>();

            List<Book> availableBooks = new() { firstAvailableBook, secondAvailableBook };
            List<Book> takenBooks = new() { firstTakenBook, secondTakenBook };
            List<Book> allBooks = new() { firstAvailableBook, secondAvailableBook, firstTakenBook, secondTakenBook };

            string option = "unknownCommand";

            var mockPrintService = new Mock<IPrintService>();
            var validationService = new ValidationService(mockPrintService.Object);

            Assert.Throws<ArgumentException>(() => validationService.ValidateFilterOption(availableBooks, takenBooks, allBooks, option))
                .Message.Should().Contain("Please check your filter option");
        }

        [Fact]
        public void ValidateTakingBook_GivenIsbnOfTakenBook_ThrowsArgumentException()
        {
            var fixture = new Fixture();

            var book = fixture.Create<Book>();
            fixture.Customize<Book>(b => b.With(p => p.TakenBy, "Joe"));
            fixture.Customize<Book>(b => b.With(p => p.ISBN, "123-cgq"));
            var books = fixture.CreateMany<Book>(2);

            var returnDate = DateTime.Now.AddMonths(1);
            var bookISBN = "123-cgq";

            var mockPrintService = new Mock<IPrintService>();
            var validationService = new ValidationService(mockPrintService.Object);

            Assert.Throws<ArgumentException>(() => validationService.ValidateTakingBook(book, returnDate, books.ToList(), bookISBN))
                .Message.Should().Contain("This book is already taken");
        }

        [Fact]
        public void ValidateBookToDelete_GivenExistingBook_ThrowsNoException()
        {
            var fixture = new Fixture();

            var firstBook = fixture.Create<Book>();
            fixture.Customize<Book>(b => b.With(p => p.ISBN, "147-tbv"));
            var secondBook = fixture.Create<Book>();

            List <Book> books = new() { firstBook, secondBook };

            var isbn = "147-tbv";

            var mockPrintService = new Mock<IPrintService>();
            var validationService = new ValidationService(mockPrintService.Object);

            var exception = Record.Exception(() => validationService.ValidateBookToDelete(isbn, books));

            Assert.Null(exception);
        }
    }
}
