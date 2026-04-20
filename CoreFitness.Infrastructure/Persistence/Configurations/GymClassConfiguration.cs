using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Configurations;

public class GymClassConfiguration : IEntityTypeConfiguration<GymClassEntity>
{

    public void Configure(EntityTypeBuilder<GymClassEntity> builder)
    {
        builder.ToTable("GymClasses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Instructor)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ScheduledTime)
            .IsRequired();

        builder.Property(x => x.Capacity)
            .IsRequired();


    }

}