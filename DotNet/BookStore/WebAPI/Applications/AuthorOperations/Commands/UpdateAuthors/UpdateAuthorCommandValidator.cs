using FluentValidation;

namespace WebAPI.Applications.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorsCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorsCommandValidator()
        {
            RuleFor(command => command.model.Name).NotEmpty();
            RuleFor(command => command.model.Surname).NotEmpty();
            RuleFor(command => command.model.BirthDate).LessThan(System.DateTime.Now.Date);
        }
    }
}