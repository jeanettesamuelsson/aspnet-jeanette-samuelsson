

using CoreFitness.Infrastructure.Identity;
using CoreFitness.Infrastructure.Persistence.Data;
using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
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

        // get Identity services
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        if (env.IsEnvironment("Testing")) // for testing
        {
            await context.Database.EnsureCreatedAsync(ct);
        }
        else // Developement (Docker) and Production
        {
            await context.Database.MigrateAsync(ct);
        }

        // run seeding methods
        await SeedRolesAsync(roleManager);
        await SeedAdminUserAsync(userManager);
        await SeedMembershipsAsync(context);
    }

    // seed data for roles

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "Member" };
        foreach (var roleName in roleNames)
        {
            // check if the role already exists, if not callCreateAsync method from roleManager
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    // seed data to create a admin user to db
    private static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
    {
        var adminEmail = "admin@corefitness.se";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser is null)
        {
            // create the user if user is null
           var user = AppUser.Create(adminEmail);

           var result = await userManager.CreateAsync(user, "ChangeMe123!");

           if (result.Succeeded)
            {
                // add admin role to user
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }


    // seed data for memberships
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
