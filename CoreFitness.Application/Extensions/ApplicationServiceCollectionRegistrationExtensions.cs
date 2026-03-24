using CoreFitness.Application.Members;
using CoreFitness.Application.Members.Services;
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

        services.AddScoped<IRegisterMemberService, RegisterMemberService>();
        services.AddScoped<IGetMemberProfileService, GetMemberProfileService>();
        services.AddScoped<ISignInMemberService, SignInMemberService>();
        services.AddScoped<IUpdateMemberProfileService, UpdateMemberProfileService>();


        return services;
    }
}
