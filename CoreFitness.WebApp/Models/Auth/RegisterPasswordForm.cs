using System.ComponentModel.DataAnnotations;

namespace CoreFitness.WebApp.Models.Auth;

public class RegisterPasswordForm
{
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Please Enter Password")]
    public string Password { get; set; } = null!;


    [Required(ErrorMessage = "Password is required to be confirmed")]
    [Compare(nameof(Password), ErrorMessage ="Passwords do not match")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Please Confirm Password")]
    public string ConfirmPassword { get; set; } = null!;


}
