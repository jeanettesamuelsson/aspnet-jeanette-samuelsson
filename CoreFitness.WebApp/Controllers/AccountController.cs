using CoreFitness.Application.Members.Inputs;
using CoreFitness.Application.Members.Services;
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
    UserManager<AppUser> userManager

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



        //    [HttpPost]
        //    [Authorize]
        //    public async Task<IActionResult> DeleteProfile(CancellationToken ct)
        //    {
        //        //get id

        //        //var userId = userManager.GetUserId(User);
        //        //var result = await deleteMemberService.ExecuteAsync(userId);




        //    }


    }

}

        