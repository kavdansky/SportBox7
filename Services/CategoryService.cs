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

        public CategoryViewModel GetCategoryArticles(string categoryNameEn)
        {

            var result = dbContext.Categories.Where(c => c.CategoryNameEN == categoryNameEn).Select(c => new CategoryViewModel() { CategoryName = c.CategoryName, CategoryNameEN = c.CategoryNameEN, CategoryId = c.Id }).FirstOrDefault();
            int[] articleIds = dbContext.Articles.Where(x => x.CategoryId == result.CategoryId).OrderByDescending(x => x.CreationDate).Select(x => x.Id).ToArray();
            foreach (var artId in articleIds)
            {
                ArticleViewModel tempModelToAdd = articleService.GetSingleArticle(artId);
                tempModelToAdd.Body = string.Join(" ", tempModelToAdd.Body.Split(' ').Take(45));
                result.Articles.Add(tempModelToAdd);
            }


            return result;
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
