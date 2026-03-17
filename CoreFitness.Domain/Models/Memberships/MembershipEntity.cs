using CoreFitness.Domain.Models.MembershipPlans;


namespace CoreFitness.Domain.Models.Memberships;

public class MembershipEntity
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;


    public virtual MembershipPlanEntity MembershipType { get; set; } = null!;
    public virtual AppUser User { get; set; } = null!;

}
