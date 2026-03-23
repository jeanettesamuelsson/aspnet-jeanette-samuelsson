using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult My()
        {
            return View();
        }
    }
}
