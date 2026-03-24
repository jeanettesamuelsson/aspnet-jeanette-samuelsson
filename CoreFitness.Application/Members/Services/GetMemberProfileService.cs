using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;

namespace CoreFitness.Application.Members.Services;

public class GetMemberProfileService(IMemberRepository memberRepository) : IGetMemberProfileService
{
    public async Task<Result<Member>> ExecuteAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User Id must be provided");

            var member = await memberRepository.GetMemberByUserIdAsync(userId, ct);


            // returns Not found if member is null, else OK result with the member object
            return member is null ? Result<Member>.NotFound($"Member with user Id: {userId} was not found ") : Result<Member>.Ok(member);

        }
        catch (Exception ex)
        {
            return Result<Member>.Error(ex.Message);
        }


    }

   

    }