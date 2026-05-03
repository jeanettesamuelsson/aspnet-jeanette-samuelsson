using CoreFitness.Application.Common.Results;

namespace CoreFitness.Application.Bookings;

public interface IDeleteBookingService
{
    Task<Result<bool>> ExecuteAsync(string bookingId, string userId, CancellationToken ct = default);
}