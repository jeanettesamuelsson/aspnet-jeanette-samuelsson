using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Identity;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;


namespace CoreFitness.Application.Members.Services;

public class RegisterMemberService(IIdentityService identityService, IMemberRepository memberRepository) : IRegisterMemberService
{
    public async Task<Result<string?>> ExecuteAsync(RegisterMemberInput input, CancellationToken ct = default)
    {
        try
        {

            if (input is null)
                throw new ArgumentException("Input must be provided");

            var createUserResult = await identityService.CreateUserAsync(input.Email, input.Password, ct);

            // if result is not successful and/ or value is null/white space -> return error message if existing, else other error message
            if (!createUserResult.Success || string.IsNullOrWhiteSpace(createUserResult.Value))
                return Result<string?>.Error(createUserResult.ErrorMessage ?? "Unable to create account");


            var member = Member.Create(createUserResult.Value);

            await memberRepository.AddAsync(member, ct);
            return Result<string?>.Ok(member.UserId);

        }
        catch (Exception ex)
        {
            return Result<string?>.Error(ex.Message);
        }


    }

    
}
