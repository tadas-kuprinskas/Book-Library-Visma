using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Commands;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;
using VismaBookLibrary.Domain.Services;
using Xunit;

namespace VismaBookLibrary.UnitTests.Commands
{
    public class CommandTests
    {
        [Theory]
        [InlineData("845-itv", "2021-06-11", "Joe")]
        public void ReturnBookCommand_GetReturnedBook_GivenBookWithCustomer_ReturnsBookWithoutCustomer(string bookISBN,
            string returnDate, string customerName)
        {
            var mockWriter = new Mock<IWriter>();

            mockWriter.Setup(r => r.ReadLine("Please enter the ISBN code of the book you want to return"))
                .Returns(bookISBN);

            mockWriter.Setup(r => r.ReadLine("Please enter the date of returning the book (year-month-day)"))
                .Returns(returnDate);

            var firstBook = new Book()
            {
                Author = "Jack London",
                Category = "Poetry",
                ISBN = bookISBN,
                Language = "English",
                Name = "Memory",
                PublicationDate = "1913",
                TakenBy = customerName,
                EstimatedReturn = DateTime.Now.AddDays(10)
            };

            var secondBook = new Book()
            {
                Author = "Edgar Alan Poe",
                Category = "Poetry",
                ISBN = "9885-pws",
                Language = "English",
                Name = "The Raven",
                PublicationDate = "1845"
            };

            List<Book> books = new() { firstBook, secondBook };

            var mockFileService = new Mock<IFileService>();
            mockFileService.Setup(g => g.GetAll()).Returns(books);

            var mockValidationService = new Mock<IValidationService>();

            ReturnBookCommand returnBookCommand = new(mockWriter.Object, mockFileService.Object, mockValidationService.Object);

            var book = returnBookCommand.GetReturnedBook();
            book.Should().NotBeNull();
            book.TakenBy.Should().BeNull();
        }

        [Fact]
        public void AddBookCommand_ReadInputToModel_GivenCorrectParameters_ReturnsBookWithCorrectISBN()
        {
            var firstBook = new Book() 
            { 
                Author = "Jack London",
                Category = "Poetry",
                ISBN = "1574-ert",
                Language = "English",
                Name = "Memory",
                PublicationDate = "1913"
            };

            var secondBook = new Book()
            {
                Author = "Edgar Alan Poe",
                Category = "Poetry",
                ISBN = "9885-pws",
                Language = "English",
                Name = "The Raven",
                PublicationDate = "1845"
            };

            var newBook = new Book()
            {
                Author = "Mark Twain",
                Category = "Luck",
                ISBN = "1549-saqg",
                Language = "English",
                Name = "Luck",
                PublicationDate = "1891"
            };

            Book nBook = new();

            string isbn = newBook.ISBN;

            List<Book> books = new() {firstBook, secondBook };

            PropertyInfo[] properties = typeof(Book).GetProperties();
            var property = properties.FirstOrDefault(p => p.Name == "ISBN");

            var mockPrintService = new Mock<IPrintService>();
            var mockWriter = new Mock<IWriter>();

            mockWriter.Setup(r => r.ReadLine($"Please enter value for {property.Name}")).Returns($"{property.GetValue(newBook)}");

            var mockFileService = new Mock<IFileService>();
            var validationService = new ValidationService(mockPrintService.Object);

            validationService.ValidateUniqueISBN(books, property, nBook, isbn);

            AddBookCommand addBookCommand = new(mockWriter.Object, mockFileService.Object, validationService);

            var book = addBookCommand.ReadInputToModel(books);

            book.Should().NotBeNull();
            book.ISBN.Should().Be(isbn);
            book.Should().BeOfType<Book>();
        }

        [Fact]
        public void TakeBookCommand_RemoveFromList_GivenCorrectParameters_ThrowsNoExceptions()
        {
            var fixture = new Fixture();

            var books = fixture.Create<IEnumerable<Book>>();
            var book = fixture.Create<Book>();

            var mockWriter = new Mock<IWriter>();
            var mockFileService = new Mock<IFileService>();
            var mockValidationService = new Mock<IValidationService>();

            TakeBookCommand takeBookCommand = new(mockWriter.Object, mockFileService.Object, mockValidationService.Object);

            var exception = Record.Exception(() => takeBookCommand.RemoveFromList(books, book));

            Assert.Null(exception);
        }

        [Theory]
        [InlineData("845-itv", "2021-06-11", "Joe")]
        public void TakeBookCommand_GetTakenBook_GivenNotTaken_ReturnsTakenBook(string bookISBN,
            string estimatedReturnTime, string customerName)
        {
            var mockWriter = new Mock<IWriter>();

            mockWriter.Setup(r => r.ReadLine("Please enter your name"))
                .Returns(customerName);

            mockWriter.Setup(r => r.ReadLine("Please enter the ISBN code of the book you want to take"))
                .Returns(bookISBN);

            mockWriter.Setup(r => r.ReadLine("Please enter estimated return date (year-month-day)"))
                .Returns(estimatedReturnTime);

            var firstBook = new Book()
            {
                Author = "Jack London",
                Category = "Poetry",
                ISBN = bookISBN,
                Language = "English",
                Name = "Memory",
                PublicationDate = "1913",
            };

            var secondBook = new Book()
            {
                Author = "Edgar Alan Poe",
                Category = "Poetry",
                ISBN = "9885-pws",
                Language = "English",
                Name = "The Raven",
                PublicationDate = "1845"
            };

            List<Book> books = new() { firstBook, secondBook };

            var mockFileService = new Mock<IFileService>();
            mockFileService.Setup(g => g.GetAll()).Returns(books);

            var mockValidationService = new Mock<IValidationService>();

            TakeBookCommand takeBookCommand = new(mockWriter.Object, mockFileService.Object, mockValidationService.Object);

            var book = takeBookCommand.GetTakenBook();
            book.Should().NotBeNull();
            book.TakenBy.Should().Be(customerName);
        }
    }
}
