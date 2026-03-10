using CoreFitness.Domain.Models;


namespace CoreFitness.Domain.Entities;

public class MembershipEntity
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string MembershipType { get; set; } = "Standard";

    public virtual AppUser User { get; set; } = null!;

}
