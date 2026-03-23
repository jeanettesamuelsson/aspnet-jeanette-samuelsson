
using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;



namespace CoreFitness.Infrastructure.Identity;

public class AppUser : IdentityUser
{
   public MemberEntity? Member { get; set; }

    public virtual MembershipEntity? Membership { get; set; }

}

