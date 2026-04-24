using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Memberships;

public class UpdateMembershipService(IMemberRepository memberRepository) : IUpdateMembershipService
{
    public async Task<Result<Member>> ExecuteAsync(string userId, string membershipId, CancellationToken ct = default)
    {
        try
        {
            var member = await memberRepository.GetMemberByUserIdAsync(userId, ct);
            if (member is null)
                return Result<Member>.NotFound("Member was not found");


            member.SetMembership(membershipId);

            var result = await memberRepository.UpdateAsync(member, ct);

            return !result
                ? Result<Member>.Error("Membership could not be updated")
                : Result<Member>.Ok(member);
        }
        catch (Exception ex)
        {
            return Result<Member>.Error(ex.Message);
        }
    }

}