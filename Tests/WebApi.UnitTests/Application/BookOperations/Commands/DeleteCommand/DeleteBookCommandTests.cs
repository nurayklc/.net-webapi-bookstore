using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteCommand
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.BookStoreDbContext;
        }

        [Theory]
        [InlineData(120)]
        [InlineData(1000)]
        [InlineData(2000)]
        [InlineData(3000)]
        public void WhenNotBookIsGiven_InvalidException_ShouldBeReturn(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunmamaktadır!");
        }
        [Fact]
        public void WhenBookIsGiven_Book_ShouldBeDeleted()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;
            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
            book.Should().BeNull();
        }
    }
}
