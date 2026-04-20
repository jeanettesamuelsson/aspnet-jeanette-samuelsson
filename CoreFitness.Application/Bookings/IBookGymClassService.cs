using CoreFitness.Application.Common.Results;

namespace CoreFitness.Application.Bookings;

public interface IBookGymClassService
{
    Task<Result<bool>> ExecuteAsync(string userId, string gymClassId, CancellationToken ct = default);
}