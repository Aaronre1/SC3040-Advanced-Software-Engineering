using System.ComponentModel.DataAnnotations;
using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Common.Security;
using ASE3040.Domain.Entities;

namespace ASE3040.Application.Features.Activities.Commands.Create;

[Authorize]
public class CreateActivityCommand : IRequest<Result>
{
    public int ItineraryId { get; set; }
    [Required]
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Required]
    public DateTime? DateTime { get; set; }
    public decimal? Cost { get; set; }
}

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    
    public CreateActivityCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }
    public async Task<Result> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var itinerary = await _context.Itineraries
            .Include(x => x.Activities)
            .Where(x => x.CreatedBy == _user.UserId)
            .Where(x => x.Id == request.ItineraryId)
            .FirstOrDefaultAsync(cancellationToken);

        if (itinerary == null)
        {
            return Result.Failure(new[] { "Itinerary is not found." });
        }

        itinerary.Activities.Add(new Activity()
        {
            Title = request.Title,
            Description = request.Description,
            DateTime = request.DateTime!.Value,
            Cost = request.Cost
        });

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}