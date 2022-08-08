using FluentValidation;

namespace WebApi.Applications.MovieOperations.Commands.DeleteMovieCommand  
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(command => command.id).GreaterThan(0);
        }
    }
}