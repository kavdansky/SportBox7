using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels.Content;

namespace SportBox7.Areas.Editors.Controllers
{
    [Authorize(Roles ="Admin, ChiefEditor, Author")]
    public class ExternalNewsSourcesController : Controller
    {
        private readonly IEditorService editorService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IExternalNewsService externalNewsService;

        public ExternalNewsSourcesController(IEditorService editorService,
            IHttpContextAccessor httpContextAccessor,
            IExternalNewsService externalNewsService)
        {
            this.editorService = editorService;
            this.httpContextAccessor = httpContextAccessor;
            this.externalNewsService = externalNewsService;
        }

        

        [Area("Editors")]
        public IActionResult Index()
        {
            return View();
        }

        [Area("Editors")]
        [HttpGet]
        public IActionResult NewsFeed()
        {
            var userPermittedCategories = editorService.GetUserCategories(httpContextAccessor).Select(c=> int.Parse(c.Value)).ToArray();
            ICollection<RawArticleViewModel> news = externalNewsService.GetExternalNews(userPermittedCategories);
            return View(news);
        }
        
        [Area("Editors")]
        [HttpGet]
        public IActionResult RawNewsDetails(int id)
        {
            var userPermittedCategories = editorService.GetUserCategories(httpContextAccessor).Select(c => int.Parse(c.Value)).ToArray();
            var result = externalNewsService.GetRawNewsDetails(id);
            return View(result);
        }
        
        [Area("Editors")]
        public async Task<IActionResult> LoadRawArticleAsDraft(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userPermittedCategories = editorService.GetUserCategories(httpContextAccessor).Select(c => int.Parse(c.Value)).ToArray();
            var result = externalNewsService.MakeRawArticleDraft(id, userId);
            return RedirectToAction("NewsFeed");
        }


    }
}