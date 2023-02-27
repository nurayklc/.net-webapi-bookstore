using FluentValidation;
namespace WebApi.Application.AuthorOperations.Queries.GetBookDetail
{
    public class GetAuthorDetailValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailValidator()
        {
            RuleFor(query => query.AuthorId).NotNull();
        }
    }
}