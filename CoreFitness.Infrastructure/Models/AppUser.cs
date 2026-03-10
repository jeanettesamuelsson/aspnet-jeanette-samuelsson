using Microsoft.AspNetCore.Identity;


namespace CoreFitness.Infrastructure.Models;

public class AppUser : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

}

