using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.WebApp.Controllers
{
    public class MembershipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
