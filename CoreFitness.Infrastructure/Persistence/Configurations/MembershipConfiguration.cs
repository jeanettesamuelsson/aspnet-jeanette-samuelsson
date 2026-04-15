
using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Persistence.Configurations;

public class MembershipConfiguration : IEntityTypeConfiguration<MembershipEntity>
{
    public void Configure(EntityTypeBuilder<MembershipEntity> builder)
    {
       
        builder.ToTable("Memberships");

        // Set PK
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(e => e.MonthlyClasses)
            .IsRequired();

    }
}

