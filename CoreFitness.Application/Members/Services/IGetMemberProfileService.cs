using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Models;

namespace CoreFitness.Application.Members.Services;

public interface IGetMemberProfileService
{
    Task<Result<Member>> ExecuteAsync(string userId, CancellationToken ct = default);
}