using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels.Users;
using SportBox7.Data.Models;

namespace SportBox7.Areas.Editors.Controllers
{

    [Area("Editors")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEditorService editorService;

        public UsersController(IUserService userService,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IEditorService editorService)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.editorService = editorService;
        }

        public IActionResult AllUsers()
        {
            return View(userService.GetAllUsers());
        }

        [HttpGet]
        public IActionResult DeactivateUser(string Id)
        {
            return View(userService.GetUser(Id));
        }

        [HttpPost]
        public IActionResult DeactivateUser(DeactivateUserViewModel model)
        {
            this.ViewData["InitialState"] = "Active";
            if (ModelState.IsValid)
            {
                if (userService.DeactivateUser(model))
                {
                    
                    return RedirectToAction("ActSuccess", new DeactivateUserViewModel() { UserName = model?.UserName, IsActive = false });
                }      
            }
            return RedirectToAction("ActFail", new DeactivateUserViewModel() { UserName = model?.UserName, IsActive = true });
        }

        [HttpGet]
        public IActionResult ActSuccess(DeactivateUserViewModel model)
        {      
            return View(model);
        }

        [HttpGet]
        public IActionResult ActFail(DeactivateUserViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult ActivateUser(string Id)
        {
            return View(userService.GetUser(Id));
        }

        [HttpPost]
        public IActionResult ActivateUser(DeactivateUserViewModel model)
        {
            this.ViewData["InitialState"] = "Inactive";
            if (ModelState.IsValid)
            {
                if (userService.ActivateUser(model))
                {
                    
                    return RedirectToAction("ActSuccess", new DeactivateUserViewModel() { UserName = model?.UserName, IsActive = true });
                }
            }
            return RedirectToAction("ActFail", new DeactivateUserViewModel() { UserName = model?.UserName, IsActive = false });
        }

      
        public IActionResult EditUser(string Id)
        {
            ViewBag.AllCategories = GetCategoriesAsListOfUserRegCategory();
            ViewBag.AllRoles = GetRolesSelectedListItems();
            return View(userService.GetUserToEdit(Id));
        }

        [HttpPost]
        public IActionResult EditUser(EditUserViewModel model)
        {
            if (ModelState.ErrorCount < 2)
            {
                userService.EditUserAsync(model).GetAwaiter().GetResult();
            }
            
            return RedirectToAction("AllUsers");
     
        }

        private List<UserRegCategory> GetCategoriesAsListOfUserRegCategory()
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

        private List<SelectListItem> GetRolesSelectedListItems()
        {
            List<SelectListItem> selectListOfRoles = new List<SelectListItem>();
            var roles = roleManager.Roles;

            foreach (var role in roles)
            {
                selectListOfRoles.Add(new SelectListItem(role.Name, role.Name));
            }

            return selectListOfRoles;
        }


    }
}