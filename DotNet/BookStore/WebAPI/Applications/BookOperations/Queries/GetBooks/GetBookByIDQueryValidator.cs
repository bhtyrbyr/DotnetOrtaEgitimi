using FluentValidation;

namespace WebAPI.Applications.BookOperations.Queries.GetBooks
{
    public class GetBookByIDQueryValidator : AbstractValidator<GetBookByIDQuery>
    {
        public GetBookByIDQueryValidator()
        {
            RuleFor(command => command.Title).NotEmpty().MinimumLength(4).MaximumLength(10);
        }
    }
}