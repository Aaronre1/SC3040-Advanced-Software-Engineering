using ASE3040.Application.Common.Interfaces;

namespace ASE3040.Application.Features.TodoLists.Commands.Create;

public class CreateToDoListCommandValidator : AbstractValidator<CreateToDoListCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public CreateToDoListCommandValidator(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle).WithMessage("'{PropertyName}' must be unique.");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.ToDoLists
            .Where(i => i.CreatedBy == _user.UserId)
            .AllAsync(i => i.Title != title, cancellationToken);
    }
}