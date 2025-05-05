

using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Business.Services;

public interface IAuthenticationService
{
    Task AddClaimByEmailAsync(MemberEntity user, string typeName, string value);
    Task<SignInResult> LoginAsync(string email, string password, bool rememberMe = false);
}

public class AuthenticationService(IAuthService authService, SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager, RoleManager<IdentityRole> roleManager) : IAuthenticationService
{
    private readonly IAuthService _authService = authService;

    private readonly UserManager<MemberEntity> _userManager = userManager;


    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe = false)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await AddClaimByEmailAsync(user, "DisplayName", $"{user.FirstName} {user.LastName}");
            }
        }

        return result;
    }

    public async Task AddClaimByEmailAsync(MemberEntity user, string typeName, string value)
    {
        if (user != null)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            if (!claims.Any(x => x.Type == typeName))
            {
                await _userManager.AddClaimAsync(user, new Claim(typeName, value));
            }
        }
    }




}