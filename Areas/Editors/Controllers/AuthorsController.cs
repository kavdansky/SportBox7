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
    [Authorize(Roles= "Author")]
    public class AuthorsController : Controller
    {

        private readonly IAuthorService authorService;
        private readonly IEditorService editorService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IValidationService validationService;

        public AuthorsController(IAuthorService authorService,
            IEditorService editorService,
            IHttpContextAccessor httpContextAccessor,
            IValidationService validationService)
        {
            this.authorService = authorService;
            this.editorService = editorService;
            this.httpContextAccessor = httpContextAccessor;
            this.validationService = validationService;
        }
        
        public IActionResult Index()
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
           
            if (ModelState.IsValid)
            {
                var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewBag.ArticleCategories = editorService.GetUserCategories(httpContextAccessor);
                model.CreatorId = userId;
                editorService.AddNewDraft(model);
                return RedirectToAction("AllDrafts");

            }          
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
        public async Task<IActionResult> SendForReview(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (validationService.CheckDraftUserPermissions(userId, id))
            {  
                authorService.SentDraftForReview(id);
                return RedirectToAction("ArticlesForReview");
            }

            return RedirectToAction("NotPermitted");
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> ArticlesForReview()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(authorService.LoadMyArticlesForReview(userId));
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> PublishedArticles()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(authorService.LoadMyPublishedArticles(userId));
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
        public async Task<IActionResult> NotPermitted()
        {
            return View();
        }

    }
}