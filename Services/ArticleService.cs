using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportBox7.Data;
using SportBox7.Data.Enums;
using SportBox7.Data.Models;
using SportBox7.Services.Interfaces;
using SportBox7.ViewModels.Articles;
using SportBox7.ViewModels.PartialViews;
using SportBox7.ViewModels.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ArticleService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


        public List<Article> GetArticlesForHomePage()
        {
            return dbContext.Articles.Include(x=> x.Category).Include(x=> x.User).Include(x=> x.SocialSignals).Include(x=> x.ArticleSeoData).Where(a=> !a.IsDeleted && a.State == ArticleState.Published).OrderByDescending(x => x.CreationDate).ToList();        
        }

        public IList<NewsWidgetViewModel> GetNewsWidget()
        {
            var articlesFromDb = dbContext.Articles.Include(x => x.Category).Include(x => x.ArticleSeoData).Where(x => !x.IsDeleted && x.State == ArticleState.Published).OrderByDescending(x => x.CreationDate);
            if (articlesFromDb == null )
            {
                return null;
            }
            List<NewsWidgetViewModel> latestArticles = articlesFromDb.Select(x=> new NewsWidgetViewModel {Title = x.Title, Href = $"/News/All/{x.Id}/{x.Category.CategoryNameEN}/{x.Title}" }).ToList();
            return latestArticles;
        }

        public ArticleViewModel GetSingleArticle(int id)
        {

            Article articleInDb = dbContext.Articles.Include(x=> x.SocialSignals).Include(x=> x.ArticleSeoData).Where(x=> x.Id == id && !x.IsDeleted && x.State == ArticleState.Published).FirstOrDefault();
            if (articleInDb == null)
            {
                return null;
            }
            ArticleViewModel model = mapper.Map<ArticleViewModel>(articleInDb);
            model.Likes[0] = articleInDb.SocialSignals.Where(x => x.IsLiked && x.ArticleId == articleInDb.Id).Count();
            model.Likes[1] = articleInDb.SocialSignals.Where(x => !x.IsLiked && x.ArticleId == articleInDb.Id).Count();
            model.MetaDescription = articleInDb.ArticleSeoData.MetaDescription;
            model.MetaKeyword = articleInDb.ArticleSeoData.MetaKeyword;
            model.MetaTitle = articleInDb.ArticleSeoData.MetaTitle;
            model.SeoUrl = articleInDb.ArticleSeoData.SeoUrl;
            model.Creator = dbContext.Users.Find(articleInDb.CreatorId).UserName;
            model.Category = dbContext.Categories.Find(articleInDb.CategoryId).CategoryName;
            model.CategoryEN = dbContext.Categories.Find(articleInDb.CategoryId).CategoryNameEN;
            return model;

        }

        public async Task<ICollection<SideBarViewModel>> GetSiteBarViewModel()
        {
            ICollection<SideBarViewModel> model = new List<SideBarViewModel>();
            List<Category> categories = dbContext.Categories.ToList();


            for (int i = 0; i < categories.Count; i++)
            {
                List<Article> currentCategoryArticles = dbContext.Articles.Include(x => x.Category).Where(x => x.CategoryId == categories[i].Id && !x.IsDeleted && x.State == ArticleState.Published).OrderByDescending(x => x.CreationDate).ToList();
                if (currentCategoryArticles.Count > 1)
                {
                    Article currentArticle = currentCategoryArticles[1];
                    model.Add(new SideBarViewModel() { ArticleId = currentArticle.Id, Category = categories[i].CategoryName, Date = currentArticle.CreationDate, Title = currentArticle.Title, ImageUrl = currentArticle.ImageUrl, CategoryNameEn = currentArticle.Category.CategoryNameEN });
                }              
            }
            return model;
        }
    }
}
