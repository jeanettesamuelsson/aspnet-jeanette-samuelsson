using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;


namespace CoreFitness.Application.Bookings;

public class BookGymClassService(

    IBookingRepository bookingRepository,
    IMemberRepository memberRepository,
    IGymClassRepository gymClassRepository) : IBookGymClassService

{
    public async Task<Result<bool>> ExecuteAsync(string userId, string gymClassId, CancellationToken ct = default)
    {
        try
        {
            // get logged in member
            var member = await memberRepository.GetMemberByUserIdAsync(userId, ct);

            if (member == null)
                return Result<bool>.Error("Could not find the member.");

            // check if the class has available spots

            var isAvailable = await gymClassRepository.IsAvailableAsync(gymClassId, ct);
            if (!isAvailable)
                return Result<bool>.Error("Gym class has hit max capacity");

            // check that logged in member is not already signed up for the class

            var alreadyBooked = await bookingRepository.BookingAlreadyExistsAsync(member.Id, gymClassId, ct);
            if (alreadyBooked)
                return Result<bool>.Error("You are already booked for this gym class.");

            // create the booking in domain model
           
            var booking = Booking.Create(member.Id, gymClassId);

            // save booking with repository

            await bookingRepository.AddAsync(booking, ct);

            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Error($"An error occured trying to create a booking: {ex.Message}");
        }
    }

}
