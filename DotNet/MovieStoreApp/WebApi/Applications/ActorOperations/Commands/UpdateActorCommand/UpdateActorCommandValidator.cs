using FluentValidation;

namespace WebApi.Applications.ActorOperations.Command.UpdateActorCommand
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(command => command.id).GreaterThan(0);
            RuleFor(command => command.model.Name).MinimumLength(3);
            RuleFor(command => command.model.Name).MaximumLength(15);
            RuleFor(command => command.model.Surname).MinimumLength(3);
            RuleFor(command => command.model.Surname).MaximumLength(15);
            RuleFor(command => command.model.Movies.Count).GreaterThan(0);
        }
    }
}