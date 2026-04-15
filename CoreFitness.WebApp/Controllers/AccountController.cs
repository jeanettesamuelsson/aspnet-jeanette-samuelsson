using CoreFitness.Application.Members.Inputs;
using CoreFitness.Application.Members.Services;
using CoreFitness.Infrastrcuture.Models;
using CoreFitness.Infrastructure.Identity;
using CoreFitness.WebApp.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.WebApp.Controllers
{
    public class AccountController(
    IGetMemberProfileService getMemberProfileService,
    IUpdateMemberProfileService updateMemberProfileService,
    IDeleteMemberService deleteMemberService,
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager

             ) : Controller


    {
        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> My(CancellationToken ct)

        {

            //get loged in user and get profile data to show in the form
            var userId = userManager.GetUserId(User);

            var member = await getMemberProfileService.ExecuteAsync(userId!, ct);

            var viewModel = new MyAccountViewModel
            {
                AboutMeForm = new MyProfileForm
                {
                    FirstName = member.Value?.FirstName ?? "",
                    LastName = member.Value?.LastName ?? "",
                    PhoneNumber = member.Value?.PhoneNumber,
                    ProfileImageUri = member.Value?.ProfileImageUri
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(MyAccountViewModel model)
        {
            var userId = userManager.GetUserId(User);

            // map from viewmodel(form) to Input application class

            var input = new UpdateMemberProfileInput(
                userId!,
                model.AboutMeForm.FirstName,
                model.AboutMeForm.LastName,
                model.AboutMeForm.PhoneNumber,
                model.AboutMeForm.ProfileFile
            );

            await updateMemberProfileService.ExecuteAsync(input);

            return RedirectToAction("My");
        }



        [HttpPost]
        public async Task<IActionResult> DeleteAccount(CancellationToken ct)
        {
            //get id

            var userId = userManager.GetUserId(User);
            var result = await deleteMemberService.ExecuteAsync(userId, ct);

            // if success, sign out and redirect to home page with success message
            if (result.Success)
            {
                await signInManager.SignOutAsync();

                TempData ["SuccessMessage"] = "Your account has been deleted successfully.";

                return RedirectToAction("Index", "Home");
            }

            // redirect to account page if deleting account failed
            return RedirectToAction("My");


        }

        [Authorize]
        [HttpGet("my/membership")]
        public async Task<IActionResult> Membership(CancellationToken ct)
        {
            var userId = userManager.GetUserId(User);

            var memberResult = await getMemberProfileService.ExecuteAsync(userId!, ct);

            if (!memberResult.Success || memberResult.Value == null)
                return NotFound();

            var member = memberResult.Value;

            var viewModel = new MyMembershipViewModel
            {
                // map from domain object to viewmodel 
              
                CurrentPlan = member.CurrentMembership != null
                    ? MapToCardViewModel(member.CurrentMembership)
                    : null,

                MemberSince = member.CreatedAt,

                // Här kan du senare lägga till AvailablePlans genom att anropa en GetMembershipsService
                AvailablePlans = new List<MembershipCardViewModel>()
            };

            return View(viewModel);
        }

        // Helper-metod för att mappa från Domän -> ViewModel
        private MembershipCardViewModel MapToCardViewModel(Membership domain)
        {
            return new MembershipCardViewModel
            {
                Id = domain.Id,
                Title = domain.Title,
                Description = domain.Description,
                Price = domain.Price.ToString("F2"), 
                Benefits = domain.Benefits,
              
            };
        }
    }

}

        