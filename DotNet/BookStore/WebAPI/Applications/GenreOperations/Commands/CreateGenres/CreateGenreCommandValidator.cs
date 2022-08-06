using System;
using FluentValidation;

namespace WebAPI.Applications.GenreOperations.Commands.CreateGenres
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(3);
            RuleFor(command => command.Model.Name).MaximumLength(15);
        }
    }
}