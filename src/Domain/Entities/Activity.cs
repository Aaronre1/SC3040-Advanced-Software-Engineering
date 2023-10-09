namespace ASE3040.Domain.Entities;

public class Activity : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateTime { get; set; }
    public bool Done { get; set; }
    public decimal? Cost { get; set; }
    public int ItineraryId { get; set; }
    public Itinerary Itinerary { get; set; } = default!;
}