using System;
using System.Collections.Generic;
using WebApi.DBOperations;
using FluentValidation;
using System.Linq;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThen(0);
        }
    }
}