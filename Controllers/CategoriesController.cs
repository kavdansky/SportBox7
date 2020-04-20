using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportBox7.Services.Interfaces;

namespace SportBox7.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IArticleService articleService;

        public CategoriesController(ICategoryService categoryService, IArticleService articleService)
        {
            this.categoryService = categoryService;
            this.articleService = articleService;
        }

        public IActionResult Details(string id)
        {
            ViewBag.NewsWidget = articleService.GetNewsWidget();
            return View(categoryService.GetCategoryArticles(id));
        }
    }
}