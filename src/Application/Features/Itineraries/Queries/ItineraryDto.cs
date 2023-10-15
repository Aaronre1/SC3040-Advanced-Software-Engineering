namespace ASE3040.Application.Features.Itineraries.Queries;

public class ItineraryDto
{
    public ItineraryDto()
    {
        Activities = Array.Empty<ActivityDto>();
    }

    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal? Budget { get; init; }
    public IReadOnlyCollection<ActivityDto> Activities { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
    public decimal? Expenses { get; init; }
    public DateTime? LastModified { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.Itinerary, ItineraryDto>();
        }
    }
}