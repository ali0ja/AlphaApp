using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Seed;

public class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<MemberEntity>>();

     

        // Skapa roller om de inte redan finns
        string[] roles = { "Admin", "Manager" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
               
            }
        }

        // Skapa användare med tilldelade roller
        await CreateUserIfNotExists(userManager, "admin@alpha.com", "Admin123!", "Admin", "System", "Admin");
        await CreateUserIfNotExists(userManager, "manager@alpha.com", "Manager123!", "Manager", "Project", "Manager");
   

        
    }

    private static async Task CreateUserIfNotExists(
        UserManager<MemberEntity> userManager,
        string email,
        string password,
        string role,
        string firstName,
        string lastName)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new MemberEntity
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                JobTitle = role,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(user, role);
        }
        else
        {
            // ✅ Uppdatera existerande användare om fälten saknas
            bool updated = false;

            if (string.IsNullOrWhiteSpace(user.FirstName)) { user.FirstName = firstName; updated = true; }
            if (string.IsNullOrWhiteSpace(user.LastName)) { user.LastName = lastName; updated = true; }
            if (string.IsNullOrWhiteSpace(user.JobTitle)) { user.JobTitle = role; updated = true; }

            if (updated)
            {
                await userManager.UpdateAsync(user);
                
            }
        }

    }
}

