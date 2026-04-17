using CoreFitness.Domain.Models;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Abstractions.Repositories;

public interface IBookingRepository : IRepositoryBase<Booking, string>
{
    // check if booking already exists for member and gym class 
    Task<bool> BookingAlreadyExistsAsync(string memberId, string gymClassId, CancellationToken ct = default);

    // get logged in members bookings
    Task<IEnumerable<Booking>> GetMemberBookingsAsync(string memberId, CancellationToken ct = default);

    // logged in member to delete a booked gym class
    Task<bool> DeleteMemberBookingAsync(string bookingId, string memberId);
}
