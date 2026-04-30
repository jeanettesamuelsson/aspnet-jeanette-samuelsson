using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Bookings;

public class GetAllBookingsService(IBookingRepository bookingRepository) : IGetAllBookingsService
{
    public async Task<Result<IEnumerable<Booking>>> ExecuteAsync(CancellationToken ct = default)
    {
        try
        {  // get all bookings from repository
            var bookings = await bookingRepository.GetAllAsync(ct);

            return Result<IEnumerable<Booking>>.Ok(bookings);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Booking>>.Error("Something went wrong trying to get bookings: " + ex.Message);
        }
    }
}