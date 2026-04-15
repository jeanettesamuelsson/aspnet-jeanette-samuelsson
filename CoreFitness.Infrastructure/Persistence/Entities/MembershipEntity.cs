using CoreFitness.Infrastructure.Identity;

namespace CoreFitness.Infrastructure.Persistence.Entities;

public sealed class MembershipEntity 
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get;set; } = null!;
    public decimal Price { get; set; }
    public int MonthlyClasses { get; set; }

    public List<string> Benefits { get; set; } = [];


}
