using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Bookings;

public class DeleteBookingService(
    IBookingRepository bookingRepository,
    IMemberRepository memberRepository) : IDeleteBookingService
{
    public async Task<Result<bool>> ExecuteAsync(string bookingId, string userId, CancellationToken ct = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(bookingId) || string.IsNullOrWhiteSpace(userId))
                return Result<bool>.Error("Booking ID and User ID must be provided.");

            var member = await memberRepository.GetMemberByUserIdAsync(userId, ct);
            if (member == null)
                return Result<bool>.NotFound("Member was not found");

            var success = await bookingRepository.DeleteMemberBookingAsync(bookingId, member.Id);

            if (!success)
            {
                return Result<bool>.Error("Something went wrong trying to delete the booking.");
            }

            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Error($"Something went wrong trying to delete the booking: {ex.Message}");
        }
    }
}
