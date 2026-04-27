using System.ComponentModel.DataAnnotations;

namespace CoreFitness.WebApp.Models.Account;

public class MyProfileForm
{
    [Required(ErrorMessage ="First name is required")]
    [Display(Name ="First Name", Prompt ="Please enter your first name")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    [Display(Name = "Last Name", Prompt = "Please enter your last name")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
    public string LastName { get; set; } = null!;

    [Phone]
    [Display(Name = "Phone Number", Prompt = "(optional) Please enter your phone number")]
    public string? PhoneNumber { get; set; } 

    [DataType(DataType.Upload)]
    [Display(Name = "Profile Image", Prompt = "(optional) Upload Profile Image")]
    public IFormFile? ProfileFile { get; set; }
    
    public string? ProfileImageUri { get; set; }




}
