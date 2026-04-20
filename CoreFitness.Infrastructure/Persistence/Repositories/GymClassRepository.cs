using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;
using CoreFitness.Infrastructure.Persistence.Data;
using CoreFitness.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;


namespace CoreFitness.Infrastructure.Persistence.Repositories;

public class GymClassRepository(DataContext context) :

    RepositoryBase<GymClass, string, GymClassEntity, DataContext>(context),
    IGymClassRepository
{
    //metod to get gym classes on a specifik date, ordered by scheduled time
    public async Task<IEnumerable<GymClass>> GetClassesByDateAsync(DateTime date, CancellationToken ct = default)
    {
        try
        { 
            var entities = await Set
                .Where(x => x.ScheduledTime.Date == date.Date)
                .OrderBy(x => x.ScheduledTime)
                .ToListAsync(ct);

            return entities.Select(ToDomainModel);
        }
        catch
        {
            throw;
        }
    }

    //method to check if a gym class is available for booking, based on its capacity and the number of bookings
    //implement later????
    public Task<bool> IsAvailableAsync(string gymClassId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    protected override void ApplyPropertyUpdates(GymClassEntity entity, GymClass model)
    {
        entity.Name = model.Name;
        entity.Instructor = model.Instructor;
        entity.Category = model.Category;
        entity.ScheduledTime = model.ScheduledTime;
        entity.Capacity = model.Capacity;
    }

    protected override string GetId(GymClass model)
    {
        return model.Id;
    }

    protected override GymClass ToDomainModel(GymClassEntity entity)
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

    protected override GymClassEntity ToEntity(GymClass model)
    {
        return new GymClassEntity
        {
            Id = model.Id,
            Name = model.Name,
            Instructor = model.Instructor,
            Category = model.Category,
            ScheduledTime = model.ScheduledTime,
            Capacity = model.Capacity
        };
    }
}
