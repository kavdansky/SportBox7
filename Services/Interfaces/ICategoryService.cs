using SportBox7.ViewModels.Articles;
using SportBox7.ViewModels.Categories;
using SportBox7.ViewModels.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Services.Interfaces
{
    public interface ICategoryService
    {
        public IList<MenuCategoryViewModel> GetMenuCategories(string currentCat);
        public IQueryable<ArticleInCategoryViewModel> GetCategoryArticles(string categoryNameEn);
    }
}
