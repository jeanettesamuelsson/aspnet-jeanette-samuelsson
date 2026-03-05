
using Microsoft.AspNetCore.Mvc;


namespace CoreFitness.WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

   
}
