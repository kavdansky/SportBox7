using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SportBox7.Data;
using SportBox7.DTO;
using SportBox7.Extensions;
using SportBox7.Services.Interfaces;
using SportBox7.ViewModels.Articles;
using SportBox7.ViewModels.Categories;

namespace SportBox7.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IArticleService articleService;
        private readonly IActionContextAccessor accessor;
        private readonly ApplicationDbContext dbContext;

        public CategoriesController(ICategoryService categoryService, IArticleService articleService, IActionContextAccessor accessor,
            ApplicationDbContext dbContext)
        {
            this.categoryService = categoryService;
            this.articleService = articleService;
            this.accessor = accessor;
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Details(string id, int? pageNumber)
        {

            if (pageNumber == null)
            {
                pageNumber = 1;
            }

            ViewBag.NewsWidget = articleService.GetNewsWidget();
            int pageSize = 3;
            IQueryable<ArticleInCategoryViewModel> model = categoryService.GetCategoryArticles(id);
            return View(await PaginatedList<ArticleInCategoryViewModel>.CreateAsync(model.AsNoTracking(), pageNumber ?? 1, pageSize).ConfigureAwait(true));
        }


    }
}