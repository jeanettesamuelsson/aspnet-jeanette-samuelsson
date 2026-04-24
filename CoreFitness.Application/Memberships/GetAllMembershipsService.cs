
using CoreFitness.Application.Common.Results;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;
using CoreFitness.Infrastrcuture.Models;

namespace CoreFitness.Application.Memberships;

public class GetAllMembershipsService(IMembershipRepository repo) : IGetAllMembershipsService
{

    public async Task<Result<Membership>> ExecuteAsync(string membershipId, CancellationToken ct = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(membershipId))
                return Result<Membership>.Error("Please enter member id");

            var membership = await repo.GetByIdAsync(membershipId, ct);

            if (membership is null)
                return Result<Membership>.NotFound($"Member with id: {membershipId} was not found");

            return Result<Membership>.Ok(membership);
        }
        catch (Exception ex)
        {
            return Result<Membership>.Error(ex.Message);
        }
    }
}

