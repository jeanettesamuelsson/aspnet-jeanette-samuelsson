using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Models;

public interface IGetGymClassesService
{
    //returns a result-object that represents a IEnumerable list of type GymClass
    Task<Result<IEnumerable<GymClass>>> ExecuteAsync(CancellationToken ct = default);
}