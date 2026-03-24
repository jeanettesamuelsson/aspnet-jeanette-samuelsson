using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.Domain.Models;

namespace CoreFitness.Application.Members.Services;

public interface IUpdateMemberProfileService
{
    Task<Result<Member>> ExecuteAsync(UpdateMemberProfileInput input, CancellationToken ct = default);
}