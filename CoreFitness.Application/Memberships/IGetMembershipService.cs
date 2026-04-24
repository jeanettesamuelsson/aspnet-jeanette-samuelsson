using CoreFitness.Application.Common.Results;
using CoreFitness.Infrastrcuture.Models;

namespace CoreFitness.Application.Memberships
{
    public interface IGetMembershipService
    {

        Task<Result<Membership>> ExecuteAsync(string membershipId, CancellationToken ct = default);
    }
}