using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Seed
{
    public static class DBInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            if (applicationBuilder != null) 
            {
                using var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                
                ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (!context.Roles.Any())
                {
                    context.Roles.Add(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN", });
                    context.Roles.Add(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "ChiefEditor", NormalizedName = "CHIEFEDITOR", });
                }
               

                context.SaveChanges();
                
            }

        }
    }
}
