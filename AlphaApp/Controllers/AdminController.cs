using Business.Services;
using Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaApp.Controllers;
[Authorize]
public class AdminController(IMemberService memberService) : Controller
{
   private readonly IMemberService _memberService = memberService;

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Projects()
    {
       
        return View();
    }

    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> Members()
    {
        var members = await _memberService.GetAllMembers();
        return View(members);
    }


    //[Authorize(Roles = "admin")]
    public IActionResult Clients()
    {
        return View();
    }

    
   
}
