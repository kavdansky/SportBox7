using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using SportBox7.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using SportBox7.Data.Seed;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Services.Interfaces;
using SportBox7.Services;
using AutoMapper;
using SportBox7.Areas.Editors.Services;
using SportBox7.Data.Models;
using SportBox7.Areas.Editors.Services.HostedServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace SportBox7
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args);

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<User>(options =>            
            options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddControllersWithViews();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                
            });
            services.AddResponseCompression(options =>
            {               
                options.Providers.Add<GzipCompressionProvider>();          
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.AddRazorPages();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IEditorService, EditorService>(); 
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IExternalNewsService, ExternalNewsService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISocialService, SocialService>();           
            services.AddHostedService<BgFootballService>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            DBInitializer.Seed(userManager, roleManager, app);
            //app.Use(async (context, next) =>
            //await context.Response.WriteAsync("Hello from middleware :)")
            //);
            app.UseStatusCodePagesWithReExecute("/Home/NotFound/{0}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseResponseCompression();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Editors",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{pageNumber?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}/{category}/{name}");

                endpoints.MapRazorPages();
            });

            

        }
    }
}
