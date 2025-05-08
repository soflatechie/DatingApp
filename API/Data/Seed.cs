using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        var pwd = "Pa$$w0rd";

        if(await userManager.Users.AnyAsync())
            return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive =  true};
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData,options);

        if(users == null)
            return;
        
        await roleManager.CreateAsync(new () {Name = "Member"});
        await roleManager.CreateAsync(new () {Name = "Admin"});
        await roleManager.CreateAsync(new () {Name = "Moderator"});

        foreach (var user in users)
        {
            user.UserName = user.UserName!.ToLower();
            await userManager.CreateAsync(user, pwd);
            await userManager.AddToRoleAsync(user, "Member");
        }

        var adminUser = new AppUser
        {
            UserName = "Admin",
            KnownAs = "Admin",
            Gender = "",
            City = "",
            Country = ""
        };
                    
        await userManager.CreateAsync(adminUser, pwd);
        await userManager.AddToRolesAsync(adminUser,["Admin","Moderator"]);
    }
}
