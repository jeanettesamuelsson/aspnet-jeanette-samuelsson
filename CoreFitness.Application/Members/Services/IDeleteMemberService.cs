using CoreFitness.Application.Common.Results;

namespace CoreFitness.Application.Members.Services
{
    public interface IDeleteMemberService
    {
        Task<Result> ExecuteAsync(string userId, CancellationToken ct = default);
    }
}