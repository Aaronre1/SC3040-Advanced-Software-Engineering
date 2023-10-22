using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Common.Security;

namespace ASE3040.Application.Features.Activities.Commands.Delete;
[Authorize]
public class DeleteActivityCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public DeleteActivityCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<Result> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .Where(x => x.CreatedBy == _user.UserId)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return Result.Failure(new[] { "Activity not found." });
        }

        _context.Activities.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}