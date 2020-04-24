using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportBox7.Services.Interfaces;

namespace SportBox7.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IArticleService articleService;
        private readonly IActionContextAccessor accessor;

        public CategoriesController(ICategoryService categoryService, IArticleService articleService, IActionContextAccessor accessor)
        {
            this.categoryService = categoryService;
            this.articleService = articleService;
            this.accessor = accessor;
        }

        public IActionResult Details(string id)
        {
            
            ViewBag.NewsWidget = articleService.GetNewsWidget();          
            return View(categoryService.GetCategoryArticles(id));
        }
    }
}