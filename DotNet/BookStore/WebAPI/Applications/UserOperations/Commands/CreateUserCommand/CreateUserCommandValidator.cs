using FluentValidation;

namespace WebAPI.Applications.UserOperations.Commands.CreateUserCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty();
            RuleFor(command => command.Model.Surname).NotEmpty();
            RuleFor(command => command.Model.Password).MinimumLength(8);
            RuleFor(command => command.Model.Password).MaximumLength(16);
            RuleFor(command => command.Model.Email).EmailAddress();
        }
    }
}