using CoreFitness.Domain.Abstractions.Repositories;
using CoreFitness.Domain.Models;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;
using CoreFitness.Infrastructure.Persistence.Data;
using CoreFitness.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

public class GymClassRepository(DataContext context) :

    RepositoryBase<GymClass, string, GymClassEntity, DataContext>(context),
    IGymClassRepository
{
    public Task<IEnumerable<GymClass>> GetClassesByDateAsync(DateTime date, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsAvailableAsync(string gymClassId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    protected override void ApplyPropertyUpdates(GymClassEntity entity, GymClass model)
    {
        throw new NotImplementedException();
    }

    protected override string GetId(GymClass model)
    {
        throw new NotImplementedException();
    }

    protected override GymClass ToDomainModel(GymClassEntity entity)
    {
        throw new NotImplementedException();
    }

    protected override GymClassEntity ToEntity(GymClass model)
    {
        throw new NotImplementedException();
    }
}
