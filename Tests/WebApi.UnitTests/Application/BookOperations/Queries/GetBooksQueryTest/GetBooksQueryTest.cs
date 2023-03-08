using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBooksQueryTest
{
    public class GetBooksQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQueryTest(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
            _context = testFixture.BookStoreDbContext;
        }

        [Fact]
        public void WhenBooksIsGiven_Books_ShouldReturnError()
        {
            // Arrange
            var query = new GetBooksQuery(_context, _mapper);

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            //result.Should().HaveCount(2);

        }
    }
}
