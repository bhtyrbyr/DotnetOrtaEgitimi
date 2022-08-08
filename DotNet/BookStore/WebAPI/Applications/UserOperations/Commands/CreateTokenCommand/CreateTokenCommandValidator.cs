using FluentValidation;

namespace WebAPI.Applications.UserOperations.Commands.CreateTokenCommand
{
    public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(command => command.Model.Email).EmailAddress();
            RuleFor(command => command.Model.Password).MinimumLength(8);
            RuleFor(command => command.Model.Password).MaximumLength(16);
        }
    }
}