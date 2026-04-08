using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.WebApp.Controllers
{
    public class CustomerServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
