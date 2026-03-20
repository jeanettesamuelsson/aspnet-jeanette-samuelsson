
using CoreFitness.Infrastrcuture.Abstractions.Repositories;

namespace CoreFitness.Application.Memberships;

public sealed class MembershipService(IMembershipRepository repo) : IMembershipService
{
}
