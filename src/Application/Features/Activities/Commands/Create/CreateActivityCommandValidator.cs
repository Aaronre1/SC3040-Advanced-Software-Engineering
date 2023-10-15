namespace ASE3040.Application.Features.Activities.Commands.Create;

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        
        RuleFor(x => x.DateTime).NotNull();
        
        When(x => x.Cost.HasValue, () => { RuleFor(x => x.Cost).GreaterThan(0); });
    }
}