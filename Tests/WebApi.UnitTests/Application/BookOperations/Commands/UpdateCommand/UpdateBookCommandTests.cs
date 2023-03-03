using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateCommand
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.BookStoreDbContext;
        }

        [Fact]
        public void WhenUpdateBookInputIsGiven_InvalidException_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 10000;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Güncellenecek Kitap Bulunamadı!");
        }
        [Fact]
        public void WhenUpdateBookIsGiven_Book_ShouldUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Model = new UpdateBookModel()
            {
                GenreId = 5,
                Title = "Test",
            };
            command.BookId = 5;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(command.Model.GenreId);
            book.Title.Should().Be(command.Model.Title);
        }
    }
}
