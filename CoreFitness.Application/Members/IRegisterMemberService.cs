using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Members.Inputs;

namespace CoreFitness.Application.Members;

public interface IRegisterMemberService
{
    Task<Result<string?>> ExecuteAsync(RegisterMemberInput input, CancellationToken ct = default);
}