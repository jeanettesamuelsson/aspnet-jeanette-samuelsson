using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

public static class RepositoryRegistrationExtension
{
    // register repos
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();


        return services;
    }
}
