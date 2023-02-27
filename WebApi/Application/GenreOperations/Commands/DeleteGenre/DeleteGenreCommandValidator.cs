using System;
using System.Collections.Generic;
using WebApi.DBOperations;
using FluentValidation;
using System.Linq;


namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
        }

    }
}