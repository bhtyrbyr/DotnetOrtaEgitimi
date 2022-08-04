using FluentValidation;
using WebAPI.DBOperations;

namespace WebAPI.BookOperations.DeleteBooks
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.title).NotEmpty().MinimumLength(4);
        }
    }
}