using CoreFitness.Application.Bookings;
using CoreFitness.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController
    (IGetAllBookingsService getAllBookingsService,
    IGetGymClassesService getGymClassesService) : Controller
{

   
    public IActionResult Index()
    {
        return View();
    }

    // show all bookings
    public async Task<IActionResult> Bookings(CancellationToken ct)
    {
        var result = await getAllBookingsService.ExecuteAsync(ct);

        if (!result.Success)
        {
            ViewData["ErrorMessage"] = result.ErrorMessage;
            return View(Enumerable.Empty<Booking>());
        }

        return View(result.Value);
    }

    // create a form to add new gym classes - get all classes and show them in a list with an option to add new ones
    public async Task<IActionResult> GymClasses(CancellationToken ct)
    {
        var result = await getGymClassesService.ExecuteAsync(ct); 

        if(!result.Success)
        {
            ViewData["ErrorMessage"] = result.ErrorMessage;
            return View(Enumerable.Empty<GymClass>());
        }

        return View(result.Value);
    }

}
