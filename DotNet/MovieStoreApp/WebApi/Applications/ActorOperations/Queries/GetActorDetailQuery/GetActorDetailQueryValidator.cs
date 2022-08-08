using FluentValidation;

namespace WebApi.Applications.ActorOperations.Queries.GetActorDetailQuery
{
    public class GetActorDetailQueryValidator : AbstractValidator<GetActorDetailQuery>
    {
        public GetActorDetailQueryValidator()
        {
            RuleFor(query => query.id).GreaterThan(0);
        }
    }
}