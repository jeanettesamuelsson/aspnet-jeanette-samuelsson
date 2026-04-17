using CoreFitness.Domain.Models;
using CoreFitness.Infrastrcuture.Abstractions.Repositories;

public interface IGymClassRepository : IRepositoryBase<GymClass, string>
{ 
    Task<IEnumerable<GymClass>> GetClassesByDateAsync(DateTime date, CancellationToken ct = default);

  
    // method to check if gym class has free spots
    Task<bool> IsAvailableAsync(string gymClassId, CancellationToken ct = default);
}