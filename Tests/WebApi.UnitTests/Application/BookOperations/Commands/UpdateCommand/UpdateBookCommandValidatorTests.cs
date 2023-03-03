using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateCommand
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandValidatorTests(CommonTestFixture fixture)
        {
            _context = fixture.BookStoreDbContext;
        }

        [Theory]
        [InlineData(" ", 100)]
        [InlineData("", 0)]
        [InlineData(" ", 100)]
        public void WhenInputBookTitleIsGiven_Validate_ShouldReturnError(string title, int genreId) 
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId
             };
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = "Lord of the Rings",
                GenreId = 1
            };
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThanOrEqualTo(0);
        }
    }
}
