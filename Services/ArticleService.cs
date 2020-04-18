using AutoMapper;
using SportBox7.Data;
using SportBox7.Data.Enums;
using SportBox7.Data.Models;
using SportBox7.Services.Interfaces;
using SportBox7.ViewModels.Articles;
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
            return dbContext.Articles.Where(a=> a.IsDeleted == false && a.State == ArticleState.Published).OrderByDescending(x => x.CreationDate).ToList();
                
        }

        public ArticleViewModel GetSingleArticle(int id)
        {

            Article articleInDb = dbContext.Articles.Find(id);
            ArticleSeoData articleSeoData = dbContext.ArticlesSeoData.Where(x=> x.ArticleId == articleInDb.Id).FirstOrDefault(); 
            ArticleViewModel model = mapper.Map<ArticleViewModel>(articleInDb);
            model.MetaDescription = articleSeoData.MetaDescription;
            model.MetaKeyword = articleSeoData.MetaKeyword;
            model.MetaTitle = articleSeoData.MetaTitle;
            model.SeoUrl = articleSeoData.SeoUrl;
            model.Creator = dbContext.Users.Find(articleInDb.CreatorId).UserName;
            model.Category = dbContext.Categories.Find(articleInDb.CategoryId).CategoryName;
            return model;

        }

        public ICollection<SideBarViewModel> GetSiteBarViewModel()
        {
            ICollection<SideBarViewModel> model = new List<SideBarViewModel>();
            List<Category> categories = dbContext.Categories.ToList();


            for (int i = 0; i < categories.Count; i++)
            {
                Article currentArticle = dbContext.Articles.Where(x => x.CategoryId == categories[i].Id).ElementAt(2);
                model.Add(new SideBarViewModel() { ArticleId = currentArticle.Id, Category = categories[i].CategoryName, Date = currentArticle.CreationDate, Title = currentArticle.Title });
            }
            return null;
        }
    }
}
