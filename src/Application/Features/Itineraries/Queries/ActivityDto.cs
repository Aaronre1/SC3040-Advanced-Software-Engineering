using ASE3040.Domain.Entities;

namespace ASE3040.Application.Features.Itineraries.Queries;

public class ActivityDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateTime { get; set; }
    public bool Done { get; set; }
    public decimal? Cost { get; set; }
    public int ItineraryId { get; set; }
    
    public DateTime? LastModified { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Activity, ActivityDto>();
        }
    }
}