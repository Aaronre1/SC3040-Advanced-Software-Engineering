using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;

namespace ASE3040.Application.Features.Activities.Commands.Edit;

public class ToggleActivityCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class ToggleActivityCommandHandler : IRequestHandler<ToggleActivityCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public ToggleActivityCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<Result> Handle(ToggleActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .Where(x => x.CreatedBy == _user.UserId)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return Result.Failure(new[] { "Activity not found." });
        }

        entity.Done = !entity.Done;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}