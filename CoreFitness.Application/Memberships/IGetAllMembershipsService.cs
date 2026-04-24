using CoreFitness.Application.Common.Results;
using CoreFitness.Infrastrcuture.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Memberships;

public interface IGetAllMembershipsService
{
    Task<Result<IEnumerable<Membership>>> ExecuteAsync(CancellationToken ct = default);

}
