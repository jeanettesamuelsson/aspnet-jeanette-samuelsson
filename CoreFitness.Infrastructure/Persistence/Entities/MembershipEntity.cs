using CoreFitness.Infrastructure.Identity;

namespace CoreFitness.Infrastructure.Persistence.Entities;

public sealed class MembershipEntity //: BaseEntity
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!; //FK
    public string Title { get; set; } = null!;
    public string Description { get;set; } = null!;
    public decimal Price { get; set; }
    public int MonthlyClasses { get; set; }

    //Navigation properties

    public ICollection<MembershipBenefitEntity> Benefits { get; set; } = [];
    public AppUser User { get; set; } = null!;


}
