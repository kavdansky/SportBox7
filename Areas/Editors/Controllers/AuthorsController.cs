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

        public AuthorsController(IAuthorService authorService, IEditorService editorService, IHttpContextAccessor httpContextAccessor)
        {
            this.authorService = authorService;
            this.editorService = editorService;
            this.httpContextAccessor = httpContextAccessor;
        }
        
        public IActionResult Index()
        {

            return View();
        }

        [Authorize]
        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> AddNewDraft()
        {
            ViewBag.ArticleCategories = editorService.GetUserCategories(httpContextAccessor);
            return View(new AddArticleViewModel());
        }


        [Authorize]
        [Area("Editors")]
        [HttpPost]
        public async Task<IActionResult> AddNewDraft(AddArticleViewModel model)
        {

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model != null)
            {
                model.CreatorId = userId;
                authorService.AddNewDraft(model);

            }

            return Redirect("/");
        }

        [Authorize]
        [Area("Editors")]
        public async Task<IActionResult> AllDrafts()
        {
            //TODO Check User and draft
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(editorService.LoadAllDrafts(userId));
        }

        [Authorize]
        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> EditDraft(int id)
        {
            //TODO Add check for article if it is draft and user permission
            ViewBag.ArticleCategories = editorService.GetUserCategories(httpContextAccessor);
            return View(authorService.EditDraftGetModel(id));
        }

        [Authorize]
        [Area("Editors")]
        [HttpPost]
        public async Task<IActionResult> EditDraft(EditArticleViewModel model)
        {
            //TODO Add check for article if it is draft and user permission
            if (ModelState.IsValid)
            {
                authorService.EditDraft(model);
            }

            return RedirectToAction("AllDrafts");
        }

        [Authorize]
        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> SentForReview(int id)
        {
            //TODO Add check for article if it is draft and user permission
            authorService.SentDraftForReview(id);
            return RedirectToAction("AllDrafts");
        }

        public async Task<IActionResult> ArticlesForReview()
        {
            //TODO Check User and draft
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(authorService.LoadMyArticlesForReview(userId));
        }







    }
}