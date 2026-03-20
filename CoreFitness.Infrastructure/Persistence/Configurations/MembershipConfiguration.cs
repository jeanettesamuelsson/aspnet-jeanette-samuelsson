using CoreFitness.Domain.Entities.Memberships;
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



        // --- RELATIONER ---


        builder.Property(e => e.UserId)
            .IsRequired();


        builder.HasMany(e => e.Benefits)       
            .WithOne(b => b.Membership)         
            .HasForeignKey(b => b.MembershipId)   
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(e => e.User)
            .WithOne(u => u.Membership)
            .HasForeignKey<MembershipEntity>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Memberships_Users");

        builder.HasIndex(e => e.UserId, "UQ_Memberships_UserId")
            .IsUnique();
    }
}

