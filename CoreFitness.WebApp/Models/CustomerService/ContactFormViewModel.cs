using System.ComponentModel.DataAnnotations;

namespace CoreFitness.WebApp.Models.CustomerService;

public class ContactFormViewModel
{
    [Display(Name = "Firstname", Prompt = "Plase enter first name")]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Lastname", Prompt = "Plase enter last name")]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Plase enter email")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please write a message")]
    [StringLength(500, ErrorMessage = "Message is too long")]
    public string Message { get; set; } = null!;
}
