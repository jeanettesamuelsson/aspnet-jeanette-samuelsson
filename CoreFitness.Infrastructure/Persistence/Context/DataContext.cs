using CoreFitness.Domain.Entities.Memberships;
using CoreFitness.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CoreFitness.Infrastructure.Persistence.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<AppUser>(options)
{

    public DbSet<MembershipEntity> Memberships => Set<MembershipEntity>();
    public DbSet<MembershipBenefitEntity> MembershipBenefits => Set<MembershipBenefitEntity>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //creates identity tables and relationships first
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

}
