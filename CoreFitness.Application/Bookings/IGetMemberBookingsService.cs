using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Models;

namespace CoreFitness.Application.Bookings;

public interface IGetMemberBookingsService
{
    Task<Result<IEnumerable<Booking>>> ExecuteAsync(string userId, CancellationToken ct = default);
}