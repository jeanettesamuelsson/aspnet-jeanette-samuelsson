
using CoreFitness.Application.Common.Results;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;
using CoreFitness.Infrastrcuture.Models;

namespace CoreFitness.Application.Memberships;

public class GetAllMembershipsService(IMembershipRepository repo) : IGetAllMembershipsService
{

    public async Task<Result<IEnumerable<Membership>>> ExecuteAsync(CancellationToken ct = default)
    {
        try
        {
            // Anropa GetAllAsync från ditt RepositoryBase
            var memberships = await repo.GetAllAsync(ct);

            if (memberships == null || !memberships.Any())
                return Result<IEnumerable<Membership>>.NotFound("Inga medlemskap hittades i databasen.");

            return Result<IEnumerable<Membership>>.Ok(memberships);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Membership>>.Error(ex.Message);
        }
    }
}

