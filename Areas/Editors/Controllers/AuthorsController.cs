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
        public async Task<IActionResult> AddArticleForReview()
        {
            ViewBag.ArticleCategories = editorService.GetUserCategories(httpContextAccessor);
            return View(new AddArticleForReviewViewModel());
        }


        [Authorize]
        [Area("Editors")]
        [HttpPost]
        public async Task<IActionResult> AddArticleForReview(AddArticleForReviewViewModel model)
        {

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model != null)
            {
                model.CreatorId = userId;
                editorService.AddArticleForReview(model);

            }



            return Redirect("/");
        }


    }
}