using System.ComponentModel.DataAnnotations;

namespace CoreFitness.WebApp.Models.Account;

public class MyProfileForm
{
    [Required(ErrorMessage ="First name is required")]
    [Display(Name ="First Name", Prompt ="Please Enter Your First Name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    [Display(Name = "Last Name", Prompt = "Please Enter Your Last Name")]
    public string LastName { get; set; } = null!;

    [Phone]
    [Display(Name = "Phone Number", Prompt = "(optional) Please Enter Your Phone Number")]
    public string? PhoneNumber { get; set; } = null!;

    [Url]
    [Display(Name = "Profile Image", Prompt = "(optional) Upload Profile Image")]
    public string? ProfielImageUri { get; set; }



}
