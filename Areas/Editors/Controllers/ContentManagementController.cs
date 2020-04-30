using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Areas.Editors.ViewModels.Content;

namespace SportBox7.Areas.Editors.Controllers
{

    [Area("Editors")]
    [Authorize(Roles = "Admin,ChiefEditor")]
    public class ContentManagementController : Controller
    {
        private readonly IEditorService editorService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IValidationService validationService;
        private readonly IAdminService adminService;


        public ContentManagementController(IEditorService editorService,
            IHttpContextAccessor httpContextAccessor,
            IValidationService validationService,
            IAdminService adminService)
        {
            this.editorService = editorService;
            this.httpContextAccessor = httpContextAccessor;
            this.validationService = validationService;
            this.adminService = adminService;
        }

        [Area("Editors")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> AddNewDraft()
        {
            ViewBag.ArticleCategories = editorService.GetUserCategories(httpContextAccessor);
            return View(new AddArticleViewModel());
        }

        [Area("Editors")]
        [HttpPost]
        public async Task<IActionResult> AddNewDraft(AddArticleViewModel model)
        {

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                model.CreatorId = userId;
                editorService.AddNewDraft(model);
                return RedirectToAction("AllDrafts");
            }
            ViewBag.ArticleCategories = editorService.GetUserCategories(httpContextAccessor);
            return View(model);
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> AllDrafts()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(editorService.LoadAllDrafts(userId));
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> EditDraft(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.ArticleCategories = editorService.GetUserCategories(httpContextAccessor);
            if (validationService.CheckDraftUserPermissions(userId, id))
            {
                var model = editorService.EditDraftGetModel(id);
                return View(model);
            }
            return RedirectToAction("NotPermitted");

        }

        [Area("Editors")]
        [HttpPost]
        public async Task<IActionResult> EditDraft(EditArticleViewModel model)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                if (validationService.CheckDraftUserPermissions(userId, model.Id))
                {
                    editorService.EditDraft(model);
                }
                return RedirectToAction("AllDrafts");
            }
            return RedirectToAction("NotPermitted");

        }
        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> ArticlesForReview()
        {       
             return View(editorService.LoadArticlesForReview());          
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> EditArticle(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.ArticleCategories = editorService.GetUserCategories(httpContextAccessor);            
            var model = adminService.EditArticleGetModel(id);
            return View(model);
        }

        [Area("Editors")]
        [HttpPost]
        public async Task<IActionResult> EditArticle(EditArticleViewModel model)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                adminService.EditArticle(model);
                return RedirectToAction("ArticlesForReview");                
            }

            return RedirectToAction("NotPermitted");
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> UnPublishArticle(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (validationService.CheckArticleUserPermissions(userId))
            {
                adminService.UnPublish(id);
            }
            return RedirectToAction("AllPublishedArticles");
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> AllPublishedArticles()
        {
           
            return View(editorService.LoadAllPublishedArticles());
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> PublishArticle(int id)
        {

            adminService.PublishArticle(id);
            return RedirectToAction("AllPublishedArticles");
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> DeleteDraft(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (validationService.CheckDraftUserPermissions(userId, id))
            {
                return View(editorService.GetDeleteDraftModel(id));
            }
            return RedirectToAction("NotPermitted");
        }

        [Area("Editors")]
        [HttpPost]
        public async Task<IActionResult> DeleteDraft(AllArticlesViewModel model)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                if (validationService.CheckDraftUserPermissions(userId, model.Id))
                {
                    editorService.DeleteDraft(model.Id);
                    return RedirectToAction("AllDrafts");
                }
            }

            return RedirectToAction("NotPermitted");
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (validationService.CheckArticleUserPermissions(userId))
            {

                return View(editorService.GetDeleteDraftModel(id));
            }
            return RedirectToAction("ArticlesForReview");
        }

        [Area("Editors")]
        [HttpPost]
        public async Task<IActionResult> DeleteArticle(AllArticlesViewModel model)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                if (validationService.CheckArticleUserPermissions(userId))
                {
                    adminService.DeleteArticle(model.Id);
                    return RedirectToAction("ArticlesForReview");
                }
            }

            return RedirectToAction("NotPermitted");
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> NotPermitted()
        {
            return View();
        }
    }
}