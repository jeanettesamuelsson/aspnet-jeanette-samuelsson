namespace CoreFitness.WebApp.Models.Account;

public class MyMembershipViewModel
{
  
    public MembershipCardViewModel? CurrentPlan { get; set; }

    public List<MembershipCardViewModel> AvailablePlans { get; set; } = new();
}
