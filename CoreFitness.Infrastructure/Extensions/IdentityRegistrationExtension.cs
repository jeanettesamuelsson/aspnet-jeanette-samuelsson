

using CoreFitness.Application.Identity;
using CoreFitness.Infrastructure.Identity;
using CoreFitness.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CoreFitness.Infrastructure.Extensions;

public static class IdentityRegistrationExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        // config email/password etc.
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;


        })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

        //config cookies
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/authentication/sign-in";
            options.AccessDeniedPath = "/authentication/sign-in";

            //make sure tempData survives an redirection
            options.Cookie.IsEssential = true;
            options.Cookie.HttpOnly = true;
        });

        //register services

        services.AddScoped<IIdentityService,  IdentityService>();

        return services;
    }
}
