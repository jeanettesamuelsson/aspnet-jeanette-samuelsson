using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.WebApp.Controllers;

public class ErrorController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("Error/{statusCode}")]
    public IActionResult ErrorHandler(int statusCode)
    {
        return statusCode switch
        {
            
            404 => View("NotFound"),
            _ => View()
        };
    }
}