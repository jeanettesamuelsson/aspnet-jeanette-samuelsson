using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.WebApp.Controllers;

public class AuthenticationController : Controller
{
    public IActionResult SignIn()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    public IActionResult RegisterPassword()
    {
        return View();
    }

    public IActionResult RegisterProfile()
    {
        return View();
    }
}
