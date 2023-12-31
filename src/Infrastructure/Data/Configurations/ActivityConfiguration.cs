using ASE3040.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASE3040.Infrastructure.Data.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DateTime)
            .IsRequired();

        builder.Property(x => x.Cost)
            .HasColumnType("money");

        builder.HasOne(x => x.Itinerary)
            .WithMany(x => x.Activities)
            .HasForeignKey(x => x.ItineraryId);
    }
}