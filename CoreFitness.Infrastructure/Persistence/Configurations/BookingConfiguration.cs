using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<BookingEntity>
{
    public void Configure(EntityTypeBuilder<BookingEntity> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookedAt)
            .IsRequired();

        builder.Property(x => x.MemberId)
            .IsRequired();

        builder.Property(x => x.GymClassId)
               .IsRequired();

        // connection to member and gym class

        builder.HasOne(x => x.Member)
               .WithMany() 
               .HasForeignKey(x => x.MemberId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(x => x.GymClass)
               .WithMany()
               .HasForeignKey(x => x.GymClassId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
