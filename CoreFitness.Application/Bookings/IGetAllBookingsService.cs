using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Bookings;

public interface IGetAllBookingsService
{
    public Task<Result<IEnumerable<Booking>>> ExecuteAsync(CancellationToken ct = default);
}
