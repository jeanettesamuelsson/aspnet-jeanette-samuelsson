using CoreFitness.Domain.Models.MembershipPlans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Persistence.Configurations;

public class MembershipPlanEntityConfiguration
{
    public void Configure (EntityTypeBuilder<MembershipPlanEntity> builder)
    {
        builder.ToTable("MembershipPlans");

        builder.HasKey(mp => mp.Id);

        builder.Property(mp => mp.MembershipPlanType)
            .HasConversion<string>();

        builder.Property(mp => mp.Price)
            .HasPrecision(10, 2);

        builder.HasMany(mp => mp.Features)
            .WithOne()
            .HasForeignKey(mp=> mp.MembershipPlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
