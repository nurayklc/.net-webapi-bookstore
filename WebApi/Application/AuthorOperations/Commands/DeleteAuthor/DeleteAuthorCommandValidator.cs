using System;
using System.Collections.Generic;
using WebApi.DBOperations;
using FluentValidation;
using System.Linq;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.DateOfBirth).NotEmpty();
        }
    }
}