namespace CoreFitness.WebApp.Models.Account;

public class MyBookingsViewModel
{
    public List<BookedClassViewModel> BookedClasses { get; set; } = [];
}

public class BookedClassViewModel
{
    public string BookingId { get; set; } = null!;
    public string ClassName { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Instructor { get; set; } = null!;
}