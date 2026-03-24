
using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;



namespace CoreFitness.Infrastructure.Identity;

public class AppUser : IdentityUser
{
   public MemberEntity? Member { get; set; }

   public virtual MembershipEntity? Membership { get; set; }


    // AppUser factory
    public static AppUser Create(string email, bool confirmed = true)
    {
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException("Email is required");

        var user = new AppUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = confirmed
        };

        return user;
    }

}

