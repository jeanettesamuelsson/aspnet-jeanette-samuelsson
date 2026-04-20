using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Bookings;

public class GetMemberBookingsService(
    IMemberRepository memberRepository,
    IBookingRepository bookingRepository) : IGetMemberBookingsService
{

    // method to get bookings for the logged in user
    public async Task<Result<IEnumerable<Booking>>> ExecuteAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            var member = await memberRepository.GetMemberByUserIdAsync(userId, ct);
            if (member == null) return Result<IEnumerable<Booking>>.NotFound("Member was not found");

            var bookings = await bookingRepository.GetMemberBookingsAsync(member.Id, ct);

            return Result<IEnumerable<Booking>>.Ok(bookings);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Booking>>.Error(ex.Message);
        }
    }
}
