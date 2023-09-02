using System;
using ASE3040.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASE3040.Infrastructure.Data.Configurations
{
	public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
	{
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}

