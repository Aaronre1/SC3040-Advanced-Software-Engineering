namespace ASE3040.Domain.Entities;

public class Itinerary : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    public DateTime? From => Activities.Select(x => x.DateTime).DefaultIfEmpty().Min();
    public DateTime? To => Activities.Select(x => x.DateTime).DefaultIfEmpty().Max();
    public decimal? Expenses => Activities.Select(x => x.Cost).DefaultIfEmpty().Sum();
}