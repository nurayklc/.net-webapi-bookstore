using FluentValidation;
namespace WebApi.Application.AuthorOperations.Queries.GetBookDetail
{
    public class GetAuthorDetailValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailValidator()
        {
            RuleFor(query => query.Id).NotNull();
            RuleFor(query => query.Name).GreaterThan(0);
        }
    }
}