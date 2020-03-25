using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Data;
using SportBox7.Data.Models;

namespace SportBox7.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class RegisterModel : PageModel
    {
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEditorService editorService;
        private readonly ApplicationDbContext dbContext;
        

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            ILogger<RegisterModel> logger,
            RoleManager<IdentityRole> roleManager,
            IEditorService editorService,
            ApplicationDbContext dbContext
            )
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            this._userManager.Options.SignIn.RequireConfirmedAccount = false;       
            this.editorService = editorService;  
            this.dbContext = dbContext;
            
            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public List<SelectListItem> Roles { get; set; }

        public List<UserRegCategory> Categories { get; set; }


        public class InputModel
        {
            public InputModel()
            {
                this.Categories = new Dictionary<int, bool>();
            }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Role { get; set; }

            public Dictionary<int, bool> Categories { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.Categories = GetCategories();
            this.Roles = GetRolesSelectedListItems();
            ReturnUrl = returnUrl;
            
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var inputCats = Input.Categories;
            returnUrl = returnUrl ?? Url.Content("~/");
            
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    _userManager.AddToRoleAsync(user, Input.Role).Wait();
                    foreach (var category in Input.Categories)
                    {                      
                        if (category.Value)
                        {
                            UserCategory userCat = new UserCategory();
                            userCat.CategoryId = category.Key;
                            userCat.UserId = user.Id;
                            dbContext.UserCategories.Add(userCat);
                        }                        
                    }

                    dbContext.SaveChanges();
                    _logger.LogInformation("User created a new account with password.");

                    return Redirect("/Editors/Admins/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private List<SelectListItem> GetRolesSelectedListItems()
        {
            List<SelectListItem> selectListOfRoles = new List<SelectListItem>();
            var roles = _roleManager.Roles;

            foreach (var role in roles)
            {
                selectListOfRoles.Add(new SelectListItem(role.Name, role.Name));
            }

            return selectListOfRoles;
        }

        private List<UserRegCategory> GetCategories() 
        {
            List<UserRegCategory> userCategories = new List<UserRegCategory>();
            List<Category> categories = editorService.GetAllCategories();
            foreach (var category in categories)
            {
                UserRegCategory currentCategory = new UserRegCategory();
                currentCategory.CategoryName = category.CategoryName;
                currentCategory.Id = category.Id;
                currentCategory.Selected = false;
                userCategories.Add(currentCategory);
            }

            return userCategories;
        }
        

        public class UserRegCategory
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public bool Selected { get; set; }
        }
    }
}
