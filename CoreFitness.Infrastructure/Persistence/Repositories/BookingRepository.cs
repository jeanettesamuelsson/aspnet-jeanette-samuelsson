using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using CoreFitness.Infrastructure.Persistence.Data;
using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

public class BookingRepository(DataContext context) :

    RepositoryBase<Booking, string, BookingEntity, DataContext>(context),
    IBookingRepository

{
    public async Task<bool> BookingAlreadyExistsAsync(string memberId, string gymClassId, CancellationToken ct = default)
    {
        return await Set.AnyAsync(b =>
            b.MemberId == memberId && b.GymClassId == gymClassId, ct);
    }

    public async Task<bool> DeleteMemberBookingAsync(string bookingId, string memberId)
    {

        try
        {
            var entity = await Set
                .FirstOrDefaultAsync(b => b.Id == bookingId && b.MemberId == memberId);

            if (entity == null)
                return false;

            Set.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch
        {
            throw;
        }

    }

    public async Task<IEnumerable<Booking>> GetMemberBookingsAsync(string memberId, CancellationToken ct = default)
    {
        try
        {
            var entities = await Set
                .Include(b => b.GymClass) // include gym class to get details
                .Where(b => b.MemberId == memberId)
                .OrderBy(b => b.GymClass!.ScheduledTime)
                .ToListAsync(ct);

            return entities.Select(ToDomainModel);
        }
        catch
        {
            throw;
        }
    }

    protected override void ApplyPropertyUpdates(BookingEntity entity, Booking model)
    {
        entity.MemberId = model.MemberId;
        entity.GymClassId = model.GymClassId;
        entity.BookedAt = model.BookedAt;
    }

    protected override string GetId(Booking model)
    {
        return model.Id;
    }

    protected override Booking ToDomainModel(BookingEntity entity)
    {
        var model = Booking.Rehydrate(
            entity.Id,
            entity.MemberId,
            entity.GymClassId,
            entity.BookedAt,
            entity.GymClass != null ? MapGymClassToDomain(entity.GymClass) : null
        );

        return model;
    }

    private GymClass? MapGymClassToDomain(GymClassEntity entity)
    {
        return GymClass.Rehydrate(
            entity.Id,
            entity.Name,
            entity.Instructor,
            entity.Category,
            entity.ScheduledTime,
            entity.Capacity
        );
    }

    protected override BookingEntity ToEntity(Booking model)
    {
        var entity = new BookingEntity
        {
            Id = model.Id,
            MemberId = model.MemberId,
            GymClassId = model.GymClassId,
            BookedAt = model.BookedAt
        };

        return entity;
    }
}
