using System.ComponentModel.DataAnnotations;
using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Common.Security;

namespace ASE3040.Application.Features.Itineraries.Commands.Edit;

[Authorize]
public class EditItineraryCommand : IRequest<Result>
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    public string? Description { get; set; }
    [DataType(DataType.Currency)]
    public decimal? Budget { get; set; }
}

public class EditItineraryCommandHandler : IRequestHandler<EditItineraryCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public EditItineraryCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<Result> Handle(EditItineraryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Itineraries
            .Where(x => x.CreatedBy == _user.UserId)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return Result.Failure(new[] { "Itinerary not found." });
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Budget = request.Budget;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}