using CoreFitness.Domain.Models;
using CoreFitness.Domain.Models.Memberships;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CoreFitness.Infrastructure.Persistence.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<AppUser>(options)
{

   public DbSet<MembershipEntity> Memberships { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //creates identity tables and relationships first
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

}
