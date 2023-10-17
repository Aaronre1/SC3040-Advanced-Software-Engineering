using ASE3040.Application.Common.Interfaces;
using ASE3040.Domain.Entities;

namespace ASE3040.Application.Features.Itineraries.Queries.Calendar;

public class GetItinerariesCalendarEvents : IRequest<IEnumerable<CalendarEventDto>>
{
}

public class GetItinerariesCalendarEventsHandler
    : IRequestHandler<GetItinerariesCalendarEvents, IEnumerable<CalendarEventDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public GetItinerariesCalendarEventsHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<IEnumerable<CalendarEventDto>> Handle(
        GetItinerariesCalendarEvents request,
        CancellationToken cancellationToken)
    {
        List<Itinerary> itineraries = await _context.Itineraries.AsNoTracking()
            .Where(i => i.CreatedBy == _user.UserId)
            .Include(i => i.Activities)
            .ToListAsync(cancellationToken);

        var result = new List<CalendarEventDto>();

        foreach (Itinerary itinerary in itineraries)
        {
            if (itinerary is { From: not null, To: not null })
            {
                result.Add(new CalendarEventDto
                {
                    Title = itinerary.Title,
                    Start = itinerary.From.Value,
                    End = itinerary.To.Value,
                    Url = $"/Itinerary/Details?id={itinerary.Id}"
                });
            }

            result.AddRange(itinerary.Activities.Select(activity => new CalendarEventDto
            {
                Title = activity.Title, Start = activity.DateTime, Url = $"/Itinerary/Details?id={itinerary.Id}"
            }));
        }

        return result;
    }
}