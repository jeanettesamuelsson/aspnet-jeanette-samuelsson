using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Models;

namespace CoreFitness.Application.Memberships;

public interface IUpdateMembershipService
{
    Task<Result<Member>> ExecuteAsync(string userId, string membershipId, CancellationToken ct = default);
}