
using CoreFitness.Infrastructure.Identity;

namespace CoreFitness.Infrastructure.Persistence.Entities;

public class MemberEntity
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUri { get; set; }

    public DateTime CreatedAt { get; set;  } 
    public DateTime? UpdatedAt { get; set; }
    public string? CurrentMembershipId { get; set; }

    public MembershipEntity? CurrentMembership { get; set; }
    public AppUser User { get; set; } = null!;


}
