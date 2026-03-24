using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;

namespace CoreFitness.Application.Members.Services;

public class UpdateMemberProfileService(IMemberRepository memberRepository) : IUpdateMemberProfileService
{
    public async Task<Result<Member>> ExecuteAsync(UpdateMemberProfileInput input, CancellationToken ct = default)
    {
        try
        {
            if (input is null)
                return Result<Member>.BadRequest("Input must be provided");

            var member = await memberRepository.GetMemberByUserIdAsync(input.UserId, ct);
            if (member is null)
                return Result<Member>.NotFound("Member was not found");

            member.UpdateInformation(input.FirstName, input.LastName, input.PhoneNumber, input.ProfileImageUri);

            //result from repo returns true or false
            var result = await memberRepository.UpdateAsync(member, ct);

            //if result is not true(not updated) -> returns Error result, else ok result with updated member object
            return !result ? Result<Member>.Error("Member was not updated") : Result<Member>.Ok(member);


        }
        catch (Exception ex)
        {
            return Result<Member>.Error(ex.Message);
        }

    }
}
