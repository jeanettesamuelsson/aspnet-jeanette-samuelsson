using CoreFitness.Infrastrcuture.Entities.Memberships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MembershipBenefitsConfiguration : IEntityTypeConfiguration<MembershipBenefitEntity>
{
    public void Configure(EntityTypeBuilder<MembershipBenefitEntity> builder)
    {

        builder.ToTable("MembershipBenefits");

       // set PK
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Benefit)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.MembershipId)
            .IsRequired();

        // one benefit has one membership, but one membership can have many benefits
        builder.HasOne(e => e.Membership)
            .WithMany(m => m.Benefits)
            .HasForeignKey(e => e.MembershipId)
            .OnDelete(DeleteBehavior.Cascade);
    }


}
