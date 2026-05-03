using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Models;

public interface IGetGymClassesService
{
    Task<Result<IEnumerable<GymClass>>> ExecuteAsync(CancellationToken ct = default);
}