

using CoreFitness.Domain.Models;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;

namespace CoreFitness.Domain.Abstractions.Repositories;

public interface IMemberRepository : IRepositoryBase<Member, string>
{
    Task<Member?> GetMemberByUserIdAsync(string userId, CancellationToken ct = default);

    string GetUserId(Member model);
}
