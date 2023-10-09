using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Common.Security;

namespace ASE3040.Application.Features.Activities.Commands.Edit;
[Authorize]
public class EditActivityCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DateTime { get; set; }
    public decimal? Cost { get; set; }
    public bool Done { get; set; }
}

public class EditActivityCommandHandler : IRequestHandler<EditActivityCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public EditActivityCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }
    
    public async Task<Result> Handle(EditActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .Where(x => x.CreatedBy == _user.UserId)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return Result.Failure(new[] { "Activity not found." });
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.DateTime = request.DateTime!.Value;
        entity.Cost = request.Cost;
        entity.Done = request.Done;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}