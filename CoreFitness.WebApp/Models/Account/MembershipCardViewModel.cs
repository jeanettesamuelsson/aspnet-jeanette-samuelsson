namespace CoreFitness.WebApp.Models.Account;

public class MembershipCardViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty; 
    public List<string> Benefits { get; set; } = new();
    public string MonthlyClasses { get; set; } = string.Empty;
}
