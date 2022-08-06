using FluentValidation;

namespace WebAPI.Applications.AuthorOperations.Commands.DeleteAuthors
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}