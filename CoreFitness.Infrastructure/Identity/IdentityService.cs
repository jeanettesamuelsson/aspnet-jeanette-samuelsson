using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Identity;
using Microsoft.AspNetCore.Identity;


namespace CoreFitness.Infrastructure.Identity;

public class IdentityService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : 
    IIdentityService


{
    public async Task<Result<string?>> CreateUserAsync(string email, string password, CancellationToken ct = default)
    {
        // check if user already exists with FindByEmail 
        var existingUser = await userManager.FindByEmailAsync(email);

        //if existing user is not null -> user already exists.
        if (existingUser is not null) 
            return Result<string?>.Conflict("Email already exists.");


        var user = AppUser.Create(email);

        var result = await userManager.CreateAsync(user, password);

        // if result is not succeeded -> return result with error, else return result with OK
        return !result.Succeeded ? Result<string?>.Error() : Result<string?>.Ok(user.Id);


    }


    public async Task<Result<bool>> PasswordSignInAsync(string email, string password, bool rememberMe, CancellationToken ct = default)
    {
       
        var result = await signInManager.PasswordSignInAsync(email, password, rememberMe, false);

        // if result not succeded -> return result with error, else return result with ok
        return !result.Succeeded ? Result<bool>.Error("Invalid email och password") : Result<bool>.Ok(true);

    }

    public Task SignOutAsync(CancellationToken ct = default)
    {
        return signInManager.SignOutAsync();
    }
}
