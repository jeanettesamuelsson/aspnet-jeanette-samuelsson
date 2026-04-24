

using CoreFitness.Infrastructure.Persistence.Data;
using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreFitness.Infrastructure.Persistence;

public static class PersistenceDatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider, IHostEnvironment env, CancellationToken ct = default)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        if (env.IsEnvironment("Testing")) // for testing
        {
            await context.Database.EnsureCreatedAsync(ct);
        }
        else // Developement (Docker) and Production
        {
            await context.Database.MigrateAsync(ct);
        }


        await SeedMembershipsAsync(context);
    }

    // seed data for memberships, add functionality for admin to add memberships manually?????
    private static async Task SeedMembershipsAsync(DataContext context)
    {
        // returns if memmberships already exist in db 
        if (await context.Memberships.AnyAsync()) return;

        var memberships = new List<MembershipEntity>
        {
            new() {
                Id = "standard-membership", 
                Title = "Standard",
                Description = "With the Standard Membership, get access to our full range of gym facilities.",
                Price = 495m,
                MonthlyClasses = 20,
                Benefits = ["Standard locker", "High-energy group fitness classes", "Motivating & supportive environment"]
            },
            new() {
                Id = "premium-membership",
                Title = "Premium",
                Description = "With the Premium Membership, get access to our full range of gym facilities.",
                Price = 595m,
                MonthlyClasses = 20, 
                Benefits = ["Priority Support & Premium Locker", "High-energy group fitness classes", "Motivating & supportive environment"]
            }
        };

        await context.Memberships.AddRangeAsync(memberships);
        await context.SaveChangesAsync();
    }
}
