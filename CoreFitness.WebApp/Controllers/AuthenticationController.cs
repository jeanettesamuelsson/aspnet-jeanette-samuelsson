using CoreFitness.Application.Identity;
using CoreFitness.Application.Members;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.WebApp.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.WebApp.Controllers;


public class AuthenticationController(

    IRegisterMemberService registerMemberService,
    ISignInMemberService signInMemberService,
    IIdentityService identityService
    
    ) : Controller
{
    // save a key value for email - HTTP context session
    private const string RegistrationEmailSessionKey = "RegistrationEmail";

    [HttpGet("sign-in")]
    public IActionResult SignIn()
    {
        return View(new SignInForm());
    }

    [HttpPost("sign-in")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(SignInForm form, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
            return View(form);

        var input = new SignInInput(form.Email, form.Password);

        var result = await signInMemberService.ExecuteAsync(input, ct);

        if (!result.Success)
        {
            ViewData["ErrorMessage"] = result.ErrorMessage;
            return View(form);

        }

        //redirect to account controller -> My
        return RedirectToAction("My", "Account");
    }

    [HttpPost("sign-out")]
    [ValidateAntiForgeryToken]
    public new async Task<IActionResult> SignOut() {

        await identityService.SignOutAsync();
        return RedirectToAction("Index", "Home");

    }

    [HttpGet("sign-up")]
    public IActionResult SignUp()
    {
        return View(new RegisterEmailForm());

    }

    [HttpPost("sign-up")]
    [ValidateAntiForgeryToken]
    public async Task< IActionResult> SignUp(RegisterEmailForm form, CancellationToken ct =default)
    {
        if (!ModelState.IsValid)
            return View(form);

        HttpContext.Session.SetString(RegistrationEmailSessionKey, form.Email);

        return RedirectToAction(nameof(SetPassword));

    }


    [HttpGet("set-password")]
    public IActionResult SetPassword()
    {
        var email = HttpContext.Session.GetString(RegistrationEmailSessionKey);

        if (string.IsNullOrWhiteSpace(email))
            return RedirectToAction(nameof(SignUp));

        return View(new RegisterPasswordForm());
    }

    [HttpPost("set-password")]
    public async Task <IActionResult> SetPassword(RegisterPasswordForm form, CancellationToken ct = default)
    {
        var email = HttpContext.Session.GetString(RegistrationEmailSessionKey);

        if (string.IsNullOrWhiteSpace(email))
            return RedirectToAction(nameof(SignUp));

        if (!ModelState.IsValid)
            return View(form);

        var registerInput = new RegisterMemberInput(email, form.Password);
        var registerResult = await registerMemberService.ExecuteAsync(registerInput, ct);
        
        if (!registerResult.Success)
        {
            ViewData["ErrorMessage"] = registerResult.ErrorMessage;

            return View(form); 
        }

        var signInInput = new SignInInput(email, form.Password);

        var signInResult = await signInMemberService.ExecuteAsync(signInInput, ct);

        if(!signInResult.Success)
        {
            ViewData["ErrorMessage"] = "Account created, but account could not be signed in";
            return View(form);
        }

        //remove key and log in user with redirect to account -> my 
        HttpContext.Session.Remove(RegistrationEmailSessionKey);
        return RedirectToAction("My", "Account");
        
    }



    //public IActionResult RegisterProfile()
    //{
    //    return View();
    //}
}
