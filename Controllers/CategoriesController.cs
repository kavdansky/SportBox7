using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
       

        public CategoriesController(ICategoryService categoryService, IArticleService articleService)
        {
            this.categoryService = categoryService;
            this.articleService = articleService;
            
            
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
            if (model == null)
            {              
                return Redirect($"/Home/NotFound");
            }
            return View(await PaginatedList<ArticleInCategoryViewModel>.CreateAsync(model.AsNoTracking(), pageNumber ?? 1, pageSize).ConfigureAwait(true));
        }


    }
}