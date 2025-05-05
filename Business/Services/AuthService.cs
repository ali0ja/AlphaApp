

using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(MemberLoginForm loginForm);
    Task LogOutAsync();
    Task<bool> SignUpAsync(MemberSignUpForm signupForm);
    Task<MemberEntity?> FindByEmailAsync(string email);
    Task ExternalLoginAsync(MemberEntity user);

}

public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;


    public async Task<bool> LoginAsync(MemberLoginForm loginForm)
    {
        var result = await _signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, false, false);
        return result.Succeeded;
    }


    public async Task<bool> SignUpAsync(MemberSignUpForm signupForm)
    {
        var memberEntity = new MemberEntity
        {
            UserName = signupForm.Email,
            FirstName = signupForm.FirstName,
            LastName = signupForm.LastName,
            PhoneNumber= signupForm.Phone,
            Email = signupForm.Email,
        };
        var result = await _userManager.CreateAsync(memberEntity, signupForm.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"{error.Code}: {error.Description}");
            }
        }
        return result.Succeeded;
    }


    public async Task LogOutAsync()
    {
        
            await _signInManager.SignOutAsync();
  
           
    
    }
    public async Task<MemberEntity?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task ExternalLoginAsync(MemberEntity user)
    {
        await _signInManager.SignInAsync(user, isPersistent: false);
    }

}


