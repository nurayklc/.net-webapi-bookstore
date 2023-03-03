using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetailQueryTest
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.BookStoreDbContext;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
        {
            // arrange
            GetBookDetailQuery query = new(_context, _mapper);
            var BookId = query.BookId = 1;

            var book = _context.Books.Include(x => x.Genre).Include(x => x.Author).Where(b => b.Id == BookId).SingleOrDefault();

            // act
            BookDetailViewModel vm = query.Handle();

            // assert
            vm.Should().NotBeNull();
            vm.Title.Should().Be(book.Title);
            vm.PageCount.Should().Be(book.PageCount);
            vm.Genre.Should().Be(book.Genre.Name);
            vm.Author.Should().Be(book.Author.Name + " " + book.Author.Surname);
            vm.PublishDate.Should().Be(book.PublishDate.ToString("dd/MM/yyyy 00:00:00"));
        }

        [Fact]
        public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            int bookId = 11;

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = bookId;

            // assert
            query.Invoking(x => x.Handle())
                 .Should().Throw<InvalidOperationException>()
                 .And.Message.Should().Be("Kitap bulunmamaktadır!");
        }
    }
}
