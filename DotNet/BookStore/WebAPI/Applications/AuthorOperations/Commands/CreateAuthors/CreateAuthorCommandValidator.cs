using FluentValidation;

namespace WebAPI.Applications.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty();
            RuleFor(command => command.Model.Surname).NotEmpty();
            RuleFor(command => command.Model.BirthDate).LessThan(System.DateTime.Now.Date);
        }
    }
}