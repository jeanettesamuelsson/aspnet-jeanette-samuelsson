using CoreFitness.Infrastructure.Identity;
using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Persistence.Configurations;

public class MemberEntityConfiguration : IEntityTypeConfiguration<MemberEntity>
{

    public void Configure(EntityTypeBuilder<MemberEntity> builder)
    {
        builder.ToTable("Members");

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.HasIndex(x => x.UserId)
            .IsUnique();

        builder.Property(x => x.CurrentMembershipId)
           .IsRequired(false);

        builder.Property(x => x.FirstName)
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
           .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
           .HasMaxLength(50);

        builder.Property(x => x.ProfileImageUri)
           .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);
           

        builder
            .HasOne(x => x.User) 
            .WithOne(x => x.Member)
            .HasForeignKey<MemberEntity>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.CurrentMembership) 
            .WithMany()                      
            .HasForeignKey(x => x.CurrentMembershipId)
            .OnDelete(DeleteBehavior.Restrict); 
    }

}

