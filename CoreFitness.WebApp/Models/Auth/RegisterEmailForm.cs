using System.ComponentModel.DataAnnotations;

namespace CoreFitness.WebApp.Models.Auth;

public class RegisterEmailForm
{
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage ="Invalid email address")]
    [Display(Name = "Email Address", Prompt ="Please Enter Email Address")]
    public string Email { get; set; } = null!;


}
