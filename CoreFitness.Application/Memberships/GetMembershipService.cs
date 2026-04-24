using CoreFitness.Application.Common.Results;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;
using CoreFitness.Infrastrcuture.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Memberships;

public class GetMembershipService(IMembershipRepository repo) : IGetMembershipService
{
    public async Task<Result<Membership>> ExecuteAsync(string membershipId, CancellationToken ct = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(membershipId))
                return Result<Membership>.Error("Please enter member id");

            var membership = await repo.GetByIdAsync(membershipId, ct);

            if (membership is null)
                return Result<Membership>.NotFound($"mmeber with id:  {membershipId} was not found");

            return Result<Membership>.Ok(membership);
        }
        catch (Exception ex)
        {
            return Result<Membership>.Error(ex.Message);
        }
    }
}
