using FluentValidation;

namespace WebApi.Applications.ActorOperations.Command.CreateActorCommand
{
    public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
            RuleFor(command => command.model.Name).MinimumLength(3);
            RuleFor(command => command.model.Name).MaximumLength(15);
            RuleFor(command => command.model.Surname).MinimumLength(3);
            RuleFor(command => command.model.Surname).MaximumLength(15);
        }
    }    
}