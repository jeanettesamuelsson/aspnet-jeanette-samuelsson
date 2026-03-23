
using Microsoft.AspNetCore.Identity;

namespace CoreFitness.Application;

public class UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;


    //public async Task<> CreateAsync() {}
    
}
