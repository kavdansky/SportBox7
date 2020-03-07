using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SportBox7.Extensions;
using SportBox7.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBox7.Data.Models;
using SportBox7.Data;
using Microsoft.AspNetCore.Identity;

namespace SportBox7.Areas.Editors.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEditorService editorService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;
        

        public HomeController(IEditorService editorService, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            this.editorService = editorService;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
                
        }

        [Authorize]
        [Area("Editors")]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        [Area("Editors")]
        public IActionResult AddArticleForReview()
        {
            ViewBag.ArticleCategories = editorService.GetUserCategories(httpContextAccessor);
            return View(new AddArticleForReviewViewModel());
        }


        [Authorize]
        [Area("Editors")]
        [HttpPost]
        public IActionResult AddArticleForReview(AddArticleForReviewViewModel model)
        {
            
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model != null)
            {
                model.CreatorId = userId;
            }
            
            editorService.AddArticleForReview(model);

            return Redirect("/");
        }

        private List<SelectListItem> GetUserCategories()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRoleId = dbContext.UserRoles.Where(x=> x.UserId == userId).FirstOrDefault().RoleId;

            List<SelectListItem> categories = new List<SelectListItem>();



            var userPermitedCategories = this.dbContext.RolesCategories.Where(x=> x.RoleId == userRoleId).ToList();


            foreach (var category in userPermitedCategories)
            {
                Category currentCategory = dbContext.Categories.Find(category.CategoryId);
                SelectListItem selListItem = new SelectListItem(currentCategory.CategoryName, currentCategory.Id+ "");
                categories.Add(selListItem);
            }

            return categories;
        }
    }
}