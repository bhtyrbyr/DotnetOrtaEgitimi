using FluentValidation;

namespace WebAPI.Applications.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.model.Name).NotEmpty();
            RuleFor(command => command.model.Surname).NotEmpty();
            RuleFor(command => command.model.BirthDate).LessThan(System.DateTime.Now.Date);
        }
    }
}