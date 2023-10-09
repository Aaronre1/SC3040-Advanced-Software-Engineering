namespace ASE3040.Application.Features.Activities.Commands.Edit;

public class EditActivityCommandValidator : AbstractValidator<EditActivityCommand>
{

    public EditActivityCommandValidator()
    {
        RuleFor(x => x.DateTime).NotNull();
        
        When(x => x.Cost.HasValue, () => { RuleFor(x => x.Cost).GreaterThan(0); });
    }
}