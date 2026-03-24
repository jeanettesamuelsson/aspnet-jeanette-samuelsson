using System.ComponentModel.DataAnnotations;

namespace CoreFitness.WebApp.Models.Auth;

public class SignInForm
{
    [Required(ErrorMessage = "Email address is required")]
    [Display(Name = "Email Address", Prompt = "Please Enter Email Address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Please Enter Password")]
    public string Password { get; set; } = null!;
    public bool RemeberMe { get; set; }




}
