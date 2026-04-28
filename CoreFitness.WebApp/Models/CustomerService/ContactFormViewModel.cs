using System.ComponentModel.DataAnnotations;

namespace CoreFitness.WebApp.Models.CustomerService;

public class ContactFormViewModel
{
    [Display(Name = "Firstname", Prompt = "Plase enter first name")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Lastname", Prompt = "Plase enter last name")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Plase enter email")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please write a message")]
    [StringLength(500, MinimumLength = 10,  ErrorMessage = "Message must be between 10 and 500 characters")]
    public string Message { get; set; } = null!;
}
