using CoreFitness.Domain.Abstractions.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

public static class RepositoryRegistrationExtension
{

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        return services;
    }
}
