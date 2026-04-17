using CoreFitness.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Bookings;

public class BookGymClassService(
    IBookingRepository bookingRepository,
    IMemberRepository memberRepository,
    IGymClassRepository gymClassRepository) : IBookGymClassService)
{
}
