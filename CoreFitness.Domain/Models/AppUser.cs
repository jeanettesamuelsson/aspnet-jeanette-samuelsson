using CoreFitness.Domain.Entities.Memberships;
using Microsoft.AspNetCore.Identity;



namespace CoreFitness.Domain.Models;

public class AppUser : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    // navigation property

    public virtual MembershipEntity? Membership { get; set; }

}

