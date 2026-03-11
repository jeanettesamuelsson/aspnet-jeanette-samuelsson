

using CoreFitness.Infrastructure.Persistence.Data;
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
    }
}
