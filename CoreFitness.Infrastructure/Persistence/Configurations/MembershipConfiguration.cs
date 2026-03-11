using CoreFitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Persistence.Configurations;

public class MembershipConfiguration : IEntityTypeConfiguration<MembershipEntity>
{
    public void Configure(EntityTypeBuilder<MembershipEntity> builder)
    {
       
        builder.ToTable("Memberships");

        // Set PK
        builder.HasKey(e => e.Id).HasName("PK_Memberships_Id");

        // Generate unique ID in db
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())"); 

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.MembershipType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Concurrency token (RowVersion) 
        builder.Property<byte[]>("Concurrency")
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();

     
        builder.Property(e => e.StartDate)
            .HasPrecision(0)
            .IsRequired()
            .HasDefaultValueSql("(SYSUTCDATETIME())")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.EndDate)
            .HasPrecision(0)
            .IsRequired(false);

        // 1 - 1 connection: AppUser <-> Membership
      
        builder.HasOne(e => e.User)
            .WithOne(u => u.Membership)
            .HasForeignKey<MembershipEntity>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Memberships_Users");

        // Unique index for UserId
        // a user can only have ONE membership
        builder.HasIndex(e => e.UserId, "UQ_Memberships_UserId")
            .IsUnique();
    }
}