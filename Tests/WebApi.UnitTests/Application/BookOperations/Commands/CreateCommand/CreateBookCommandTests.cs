using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entity;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.BookStoreDbContext;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange(hazırlık)
            var book = new Book()
            {
                Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new DateTime(1980, 2, 25),
                GenreId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };

            //act (çalıştırma) & assert(doğrulama)
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut!");

            //assert(doğrulama)
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeCreated()
        {
            //arrange(hazırlık)
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel book = new CreateBookModel()
            {
                Title = "Testtt",
                PageCount = 100,
                PublishDate = new DateTime(1980, 2, 25),
                GenreId = 1
            };
            command.Model = book;

            //act (çalıştırma) 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert(doğrulama)
            var bookResult = _context.Books.SingleOrDefault(x => x.Title == book.Title);
            bookResult.Should().NotBeNull();
            bookResult.PageCount.Should().Be(book.PageCount);
            bookResult.PublishDate.Should().Be(book.PublishDate);
            bookResult.GenreId.Should().Be(book.GenreId);
        }
    }
}
