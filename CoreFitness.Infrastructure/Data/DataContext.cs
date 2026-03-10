using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CoreFitness.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<AppUser>(options)
{

   public DbSet<MembershipEntity> Memberships { get; set; }

}
