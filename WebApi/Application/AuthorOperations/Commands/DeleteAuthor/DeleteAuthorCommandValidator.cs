using System;
using System.Collections.Generic;
using WebApi.DBOperations;
using FluentValidation;
using System.Linq;
using WebApi.AuthorOperations.DeleteAuthor;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).NotNull().NotEmpty();
        }
    }
}