using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Models.MembershipPlans;

public  class MembershipPlanFeaturesEntity
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public int SortOrder { get; set; }


    public Guid MembershipPlanId { get; set; }

  
}
    