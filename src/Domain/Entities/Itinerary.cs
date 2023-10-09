namespace ASE3040.Domain.Entities;

public class Itinerary : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    public DateTime? From => Activities.Min(x => x.DateTime);
    public DateTime? To => Activities.Max(x => x.DateTime);
    public decimal? Expenses => Activities.Sum(x => x.Cost);
}