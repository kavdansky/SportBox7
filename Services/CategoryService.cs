using Microsoft.EntityFrameworkCore;
using SportBox7.Data;
using SportBox7.Services.Interfaces;
using SportBox7.ViewModels.Articles;
using SportBox7.ViewModels.Categories;
using SportBox7.ViewModels.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IArticleService articleService;

        public CategoryService(ApplicationDbContext dbContext, IArticleService articleService)
        {
            this.dbContext = dbContext;
            this.articleService = articleService;
        }

        public IQueryable<ArticleInCategoryViewModel> GetCategoryArticles(string categoryNameEn)
        {

            var category = dbContext.Categories.Where(c => c.CategoryNameEN == categoryNameEn).Select(c => new CategoryViewModel() { CategoryName = c.CategoryName, CategoryNameEN = c.CategoryNameEN, CategoryId = c.Id }).FirstOrDefault();
           
            IQueryable<ArticleInCategoryViewModel> articles = dbContext.Articles.Include(x=> x.User).Include(x=> x.ArticleSeoData).Where(x => x.CategoryId == category.CategoryId).OrderByDescending(x => x.CreationDate).Select(x => new ArticleInCategoryViewModel {Body = x.Body, Category = category.CategoryName, CategoryId = category.CategoryId, CategoryEN = category.CategoryNameEN, CreationDate = x.CreationDate, Creator = x.User.UserName, H1Tag = x.H1Tag, Id = x.Id, ImageUrl =x.ImageUrl, SeoUrl = x.ArticleSeoData.SeoUrl });
           


            return articles;
        }


        public IList<MenuCategoryViewModel> GetMenuCategories(string currentCat)

        {
            List<MenuCategoryViewModel> categories = new List<MenuCategoryViewModel>();
            categories.Add(new MenuCategoryViewModel {isCurrent = false, NameBg = "Начало", NameEn = "Home" });
            categories.AddRange(dbContext.Categories.Select(c => new MenuCategoryViewModel() { NameBg = c.CategoryName, NameEn = c.CategoryNameEN }).ToList());
           
            for (int i = 0; i < categories.Count; i++)
             {
                 if (categories[i].NameEn == currentCat)
                 {
                     categories[i].isCurrent = true;
                 }               
             }
             return categories;
        }

        
    }
}
