using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Members.Inputs;

namespace CoreFitness.Application.Members;

public interface IRegisterMemberService
{
    Task<Result<string?>> ExecuteAsync(RegisterMemberAccountInput input, CancellationToken ct = default);
}