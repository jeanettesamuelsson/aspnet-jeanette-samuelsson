using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using CoreFitness.Infrastructure.Persistence.Data;
using CoreFitness.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

public class BookingRepository(DataContext context) :

    RepositoryBase<Booking, string, BookingEntity, DataContext>(context),
    IBookingRepository

{
    public Task<bool> BookingAlreadyExistsAsync(string memberId, string gymClassId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteMemberBookingAsync(string bookingId, string memberId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Booking>> GetMemberBookingsAsync(string memberId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    protected override void ApplyPropertyUpdates(BookingEntity entity, Booking model)
    {
        throw new NotImplementedException();
    }

    protected override string GetId(Booking model)
    {
        throw new NotImplementedException();
    }

    protected override Booking ToDomainModel(BookingEntity entity)
    {
        throw new NotImplementedException();
    }

    protected override BookingEntity ToEntity(Booking model)
    {
        throw new NotImplementedException();
    }
}
