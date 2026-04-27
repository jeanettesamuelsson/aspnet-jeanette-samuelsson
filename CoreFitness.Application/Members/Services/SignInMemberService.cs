using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Identity;
using CoreFitness.Application.Members.Inputs;

namespace CoreFitness.Application.Members.Services;

public class SignInMemberService(IIdentityService identityService) : ISignInMemberService
{
    public async Task<Result> ExecuteAsync(SignInInput input, CancellationToken ct = default)
    {
        try
        {
            if (input is null)
                throw new ArgumentException("Input must be provided");

            var result = await identityService.PasswordSignInAsync(input.Email, input.Password, ct);

            return !result.Success ? Result.Error(result.ErrorMessage ?? "Invalid email or password") : Result.Ok();

        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }


    }
}