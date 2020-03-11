using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportBox7.Data.Seed
{
    public static class DBInitializer
    {

        public static void Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IApplicationBuilder app)
        {
            SeedRoles(roleManager);
            Thread.Sleep(1000);
            SeedUsers(userManager);
            Thread.Sleep(1000);
            SeedCategoties(app);
            Thread.Sleep(1000);
            SeedUserPermitedCategoties(app);
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

        public static void SeedCategoties(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder?.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (context.Categories?.Count() < 1)
            {
                Category category1 = new Category();
                category1.CategoryName = "Футбол БГ";
                context.Add(category1);

                Category category2 = new Category();
                category2.CategoryName = "Футбол свят";
                context.Add(category2);

                Category category3 = new Category();
                category3.CategoryName = "Баскетбол";
                context.Add(category3);

                Category category4 = new Category();
                category4.CategoryName = "Бойни";
                context.Add(category4);

                Category category5 = new Category();
                category5.CategoryName = "Други спортове";
                context.Add(category5);

                context.SaveChanges();
            }

        }

        public static void SeedUserPermitedCategoties(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder?.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (context.RolesCategories?.Count() < 1)
            {


                var AdminRole = context.Roles.Where(x => x.Name == "Admin").FirstOrDefault();
                var categories = context.Categories.ToList();

                
                for (int p = 0; p < categories.Count; p++)
                {
                    RoleCategory roleCat = new RoleCategory();
                    roleCat.RoleId = AdminRole.Id;
                    roleCat.CategoryId = categories[p].Id;
                    context.RolesCategories.Add(roleCat);
                    
                }
                    context.SaveChanges();

                
            }
            


        }
    }
}
