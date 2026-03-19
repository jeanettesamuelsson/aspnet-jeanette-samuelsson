namespace CoreFitness.Domain.Models.MembershipPlans;

public class MembershipPlanEntity
{
    public MembershipPlanType MembershipPlanType { get; set; }
    public string Description { get; set; } = null!;
    public ICollection<MembershipPlanFeaturesEntity> Features { get; set; } = [];
    public decimal Price { get; set; }
    public int MonthlyClassLimit { get; set; }
    public int FreeTrials { get; set; }
    public int SortOrder { get; set; }





}
