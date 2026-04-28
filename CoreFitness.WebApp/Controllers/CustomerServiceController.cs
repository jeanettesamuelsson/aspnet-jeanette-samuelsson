using CoreFitness.WebApp.Models.CustomerService;
using Microsoft.AspNetCore.Mvc;



namespace CoreFitness.WebApp.Controllers;

public class CustomerServiceController : Controller
{

    //send in empty model 
    [HttpGet]
    public IActionResult Index()
    {
        
        return View(new ContactFormViewModel());
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken] 
    public IActionResult Index(ContactFormViewModel form)
    {
        //reurn form view with error message if modelstate is not valid
        if (!ModelState.IsValid)
        { 
            return View(form);
        }

        TempData["SuccessMessage"] = "Thank you for contacting us! We will get back to you soon.";

        return RedirectToAction("Index");
    }
}
