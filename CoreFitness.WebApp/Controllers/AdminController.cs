using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.WebApp.Controllers;

public class AdminController : Controller
{

    //test for admin 
    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }

    //// show all bookings
    //public async Task<IActionResult> Bookings()
    //{
    //    return View();
    //}

    //// create a form to add new gym classes
    //public IActionResult CreateSession()
    //{
    //    return View();
    //}

}
