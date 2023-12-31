using ASE3040.Application.Common.Interfaces;

namespace ASE3040.Application.Features.Itineraries.Commands.Edit;

public class EditItineraryCommandValidator : AbstractValidator<EditItineraryCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public EditItineraryCommandValidator(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;

        RuleFor(x => x)
            .MustAsync(BeUniqueTitle)
            .WithName(nameof(EditItineraryCommand.Title))
            .WithMessage("'{PropertyName}' must be unique.");;

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        When(x => x.Budget.HasValue, () => { RuleFor(x => x.Budget).GreaterThan(0); });
    }

    private async Task<bool> BeUniqueTitle(EditItineraryCommand request, CancellationToken cancellationToken)
    {
        return await _context.Itineraries
            .Where(i => i.CreatedBy == _user.UserId)
            .Where(i => i.Id != request.Id)
            .AllAsync(i => i.Title != request.Title, cancellationToken);
    }
}