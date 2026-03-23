using CoreFitness.Infrastructure.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace CoreFitness.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionRegistrationExtensions
{


    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env )
    {
        services.AddPersistence(configuration, env);
        services.AddIdentityServices();


        return services;
    }
}
