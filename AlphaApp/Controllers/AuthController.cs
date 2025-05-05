
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlphaApp.Controllers;

public class AuthController(IAuthService authService, SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : Controller
{
    private readonly IAuthService _authService = authService;

    private readonly UserManager<MemberEntity> _userManager = userManager;

   
    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = "";
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(MemberLoginForm form, string returnUrl="~/")
    {
        ViewBag.ErrorMessage = "";
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(form);
            if (result)
            {
                return LocalRedirect(returnUrl);
            }
        }
        ViewBag.ErrorMessage = "Incorrect email or password .";
        return View(form);
        
    }
    public IActionResult SignUp()
    {
        ViewBag.ErrorMessage = "";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(MemberSignUpForm form)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.SignUpAsync(form);
            if (result)
            {
                return LocalRedirect("~/");
            }
        }
        ViewBag.ErrorMessage = "";
        return View(form);

    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogOutAsync();
        return LocalRedirect("~/");
    }


    public IActionResult Denid()
    {
        return View();
    }

    //[HttpPost]
    //public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
    //{
    //    if (string.IsNullOrEmpty(provider))
    //    {
    //        ModelState.AddModelError("", "Invalid provider");
    //        return View("LogIn");
    //    }

    //    var redirectUrl = Url.Action("ExternalSignInCallback", "Auth", new { returnUrl });
    //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    //    return Challenge(properties, provider);
    //}


    //public async Task<ActionResult> ExternalSignInCallback(string returnUrl = null!, string remoteError = null!)
    //{
    //    returnUrl ??= Url.Content("~/");

    //    if (!string.IsNullOrEmpty(remoteError))
    //    {
    //        ModelState.AddModelError("", $"Error from external provider: {remoteError}");
    //        return View("LogIn");
    //    }

    //    var info = await _signInManager.GetExternalLoginInfoAsync();
    //    if (info == null)
    //    {
    //        return RedirectToAction("SignIn");
    //    }

    //    var signInResult = await signInManager.ExternalLoginSignInAsync(
    //        info.LoginProvider,
    //        info.ProviderKey,
    //        isPersistent: false,
    //        bypassTwoFactor: true);

    //    if (signInResult.Succeeded)
    //    {
    //        return LocalRedirect(returnUrl);
    //    }
    //    else
    //    {
    //        string firstName = string.Empty;
    //        string lastName = string.Empty;

    //        try
    //        {
    //            firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
    //        }
    //        catch
    //        {
    //            // Optional: log or handle missing claim
    //        }

    //        string lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
    //        string email = info.Principal.FindFirstValue(ClaimTypes.Email);
    //        string username = $"{info.LoginProvider.ToLower()}{email}";

    //        var user = new AppIdentityUser
    //        {
    //            UserName = username,
    //            Email = email,
    //            FirstName = firstName,
    //            LastName = lastName
    //        };
    //    }
    //}
}



