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
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidBook_Validator_ShouldBeReturnError()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = -1;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();   
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGivenBook_Validator_ShouldNotBeError()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}
