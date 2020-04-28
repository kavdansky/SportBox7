using SportBox7.Data.Models;
using SportBox7.ViewModels.Articles;
using SportBox7.ViewModels.PartialViews;
using SportBox7.ViewModels.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Services.Interfaces
{
    public interface IArticleService
    {
        List<ArticleViewModel> GetArticlesForHomePage();

        ArticleViewModel GetSingleArticle(int id);

        Task<ICollection<SideBarViewModel>> GetSiteBarViewModel();

        IList<NewsWidgetViewModel> GetNewsWidget();
    }
}
