using CoreFitness.Application.Common.Results;
using CoreFitness.Application.Members.Inputs;

namespace CoreFitness.Application.Members;

public interface ISignInMemberService
{
    Task<Result> ExecuteAsync(SignInInput input, CancellationToken ct = default);
}