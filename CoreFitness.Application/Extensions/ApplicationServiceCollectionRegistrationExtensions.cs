using CoreFitness.Application.Memberships;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CoreFitness.Application.Extensions;

public static class ApplicationServiceCollectionRegistrationExtensions
{


    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddScoped<IMembershipService, MembershipService>();
        return services;
    }
}
