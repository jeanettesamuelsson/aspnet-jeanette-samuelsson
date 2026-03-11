using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Context;

public static class ContextRegistrationExtension
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        return services;
    }
}
