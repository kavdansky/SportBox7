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

namespace SportBox7.Areas.Editors.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEditorService editorService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HomeController(IEditorService editorService, IHttpContextAccessor httpContextAccessor)
        {
            this.editorService = editorService;
            this.httpContextAccessor = httpContextAccessor;
                
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
            return View(new AddArticleForReviewViewModel());
        }


        [Authorize]
        [Area("Editors")]
        [HttpPost]
        public IActionResult AddArticleForReview(AddArticleForReviewViewModel model)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (model !=null)
            {
                model.CreatorId = userId;
                editorService.AddArticleForReview(model);
            }
            
            
            return View();
        }


    }
}