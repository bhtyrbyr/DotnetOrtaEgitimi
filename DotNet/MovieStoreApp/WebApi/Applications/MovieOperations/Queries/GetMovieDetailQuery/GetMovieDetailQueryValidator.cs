using FluentValidation;

namespace WebApi.Applications.MovieOperations.Queries.GetMovieDetailQuery
{
    public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0);
        }
    }
}