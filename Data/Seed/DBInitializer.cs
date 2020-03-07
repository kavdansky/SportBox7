using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportBox7.Data.Seed
{
    public static class DBInitializer
    {

        public static void Seed(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            Thread.Sleep(1000);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager?.FindByNameAsync("kavdansky@mail.bg").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "kavdansky@mail.bg";
                user.Email = "kavdansky@mail.bg";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Kavdansky1!").Result;
                
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"Admin").Wait();
                }
            }


            if (userManager?.FindByNameAsync("rusulski@mail.bg").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "rusulski@mail.bg";
                user.Email = "rusulski@mail.bg";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync
                (user, "Rusulski1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "ChiefEditor").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("ChiefEditor").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "ChiefEditor";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Author").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Author";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
