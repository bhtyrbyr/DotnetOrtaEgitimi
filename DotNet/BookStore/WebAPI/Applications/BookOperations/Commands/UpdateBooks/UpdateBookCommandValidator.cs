
using System;
using FluentValidation;

namespace WebAPI.Applications.BookOperations.Commands.UpdateBooks
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate).LessThan(DateTime.Now.Date);
        }
    }
}