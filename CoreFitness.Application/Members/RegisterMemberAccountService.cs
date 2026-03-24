using CoreFitness.Application.Members.Inputs;
using CoreFitness.Domain.Abstractions.Repositories;


namespace CoreFitness.Application.Members;

public class RegisterMemberAccountService(IIdentityService identityService, IMemberRepository memberRepository)
{
    public async Task<Result<string?>> ExecuteAsync(RegisterMemberAccountInput input, CancellationToken ct = default);
}
