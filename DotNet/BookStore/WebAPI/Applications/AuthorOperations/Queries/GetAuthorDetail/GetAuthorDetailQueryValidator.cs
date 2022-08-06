using FluentValidation;

namespace WebAPI.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}