using CoreFitness.Application.Common.Results;
using CoreFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

public class GetGymClassesService(IGymClassRepository gymClassRepository) : IGetGymClassesService
{
    public async Task<Result<IEnumerable<GymClass>>> ExecuteAsync(CancellationToken ct = default)
    {
        try
        {
            // get list of classes from repo
            var gymClasses = await gymClassRepository.GetAllAsync(ct);

            return Result<IEnumerable<GymClass>>.Ok(gymClasses);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<GymClass>>.Error("Something went wrong trying to get gym classes: " + ex.Message);
        }
    }

  
}