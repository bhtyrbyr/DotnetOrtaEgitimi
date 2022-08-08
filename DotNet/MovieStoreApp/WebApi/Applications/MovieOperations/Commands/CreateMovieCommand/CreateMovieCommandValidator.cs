using FluentValidation;

namespace WebApi.Applications.MovieOperations.Commands.CreateMovieCommand
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(command => command.model.Name).MinimumLength(4);
            RuleFor(command => command.model.Name).MaximumLength(20);
            RuleFor(command => command.model.Price).GreaterThan(15);
            RuleFor(command => command.model.Director.Name).MinimumLength(3);
            RuleFor(command => command.model.Director.Name).MaximumLength(15);
            RuleFor(command => command.model.Director.Surname).MinimumLength(3);
            RuleFor(command => command.model.Director.Surname).MaximumLength(15);
            RuleFor(command => command.model.Genres.Count).GreaterThan(0);
            RuleFor(command => command.model.Actors.Count).GreaterThan(0);
        }
    }
}