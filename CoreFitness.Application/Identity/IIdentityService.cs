
using CoreFitness.Application.Common.Results;

namespace CoreFitness.Application.Identity;

public interface IIdentityService
{
    Task<Result<string?>> CreateUserAsync(string email, string password, CancellationToken ct = default);

    Task<Result<bool>> PasswordSignInAsync(string email, string password, bool rememberMe, CancellationToken ct = default);

    Task SignOutAsync(CancellationToken ct = default);

    Task<Result> DeleteUserAsync(string userId, CancellationToken ct = default);

}
 