using CoreFitness.Application.Memberships;
using CoreFitness.WebApp.Models.Account;
using Microsoft.AspNetCore.Mvc;


namespace CoreFitness.WebApp.Controllers
{
    public class MembershipController(IGetAllMembershipsService getAllMembershipsService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            // get memberships
            var result = await getAllMembershipsService.ExecuteAsync(ct);

            // return empty list if null
            if (!result.Success || result.Value == null)
            {
                return View(new List<MembershipCardViewModel>()); 
            }

            //map from domain model to viewmodel
            var viewModel = result.Value.Select(m => new MembershipCardViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Price = m.Price.ToString("F2"), 
                Benefits = m.Benefits ?? new List<string>(),
                MonthlyClasses = m.MonthlyClasses.ToString()
            }).ToList();

            return View(viewModel);
        }




    }
}
