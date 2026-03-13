using CoreFitness.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace CoreFitness.Infrastructure.Persistence.Context;

public static class ContextRegistrationExtension
{

    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            Console.WriteLine("Development Environment - Using Local SQLite File");

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(connectionString));
        }

        else if (env.IsEnvironment("Testing"))
        {
            Console.WriteLine("Test Environment - Using SQLite In-Memory");

            // create connection to in-memory SQLite database
            var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=:memory:");
            connection.Open();

            // register connection as singleton so it wont be disposed and closed
            services.AddSingleton(connection);

            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(connection));
        }

        else
        {
            Console.WriteLine("Production Environment - Using Production SQL Server");

            var connectionString = configuration.GetConnectionString("ProductionDatabaseUri")
                ?? throw new Exception("Production Database Uri not Provided");

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));
        }

        return services;
    }

}
