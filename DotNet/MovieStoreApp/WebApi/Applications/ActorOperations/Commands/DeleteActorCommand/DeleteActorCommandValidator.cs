using FluentValidation;

namespace WebApi.Applications.ActorOperations.Command.DeleteActorCommand
{
    public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(command => command.id).GreaterThan(0);
        }
    }
}