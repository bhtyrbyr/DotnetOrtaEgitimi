using FluentValidation;

namespace WebAPI.BookOperations.GetBooks
{
    public class GetBookByIDQueryValidator : AbstractValidator<GetBookByIDQuery>
    {
        public GetBookByIDQueryValidator()
        {
            RuleFor(command => command.Title).NotEmpty().MinimumLength(4).MaximumLength(10);
        }
    }
}