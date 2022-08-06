using FluentValidation;

namespace WebAPI.Applications.GenreOperations.Commands.UpdateGenres
{
    class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreID).GreaterThan(0);
            RuleFor(command => command.model.Name).MinimumLength(4).When(x => x.model.Name.Trim() != string.Empty);
        }
    }
}