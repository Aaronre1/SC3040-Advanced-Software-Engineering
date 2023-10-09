using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Common.Security;

namespace ASE3040.Application.Features.Itineraries.Commands.Create;

[Authorize]
public class CreateItineraryCommand : IRequest<Result>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
}

public class CreateItineraryCommandHandler : IRequestHandler<CreateItineraryCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateItineraryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> Handle(CreateItineraryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Itinerary
        {
            Title = request.Title,
            Description = request.Description,
            Budget = request.Budget
        };

        _context.Itineraries.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}