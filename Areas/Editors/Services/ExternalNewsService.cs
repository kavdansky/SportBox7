using AutoMapper;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels.Content;
using SportBox7.Data;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services
{
    public class ExternalNewsService : IExternalNewsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ExternalNewsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ICollection<RawArticleViewModel> GetExternalNews(int[] userPermittedCats)
        {
            List<RawArticleViewModel> rawArticlesToReturn = new List<RawArticleViewModel>();

            foreach (var category in userPermittedCats)
            {
                string currentCategoryAsString = dbContext.Categories.Find(category).CategoryName;
                List<RawArticleViewModel> rawArticlesForCurrentCat = dbContext.RawArticles.Where(ra => ra.CategoryId == category && !ra.IsDeleted).Select(x => new RawArticleViewModel {Body = x.Body, CategoryId = x.CategoryId, Date = x.Date, H1Tag = x.H1Tag, Id = x.Id, ImageUrl = x.ImageUrl, SourceName = x.SourceName, SourceURL = x.SourceURL, Title = x.Title, Category = currentCategoryAsString }).ToList();

                foreach (var rawArticle in rawArticlesForCurrentCat)
                {
                    rawArticlesToReturn.Add(rawArticle);
                }
            }

            return rawArticlesToReturn.OrderByDescending(x => x.Date).ToList();
        }

        public RawArticleViewModel GetRawNewsDetails(int id)
        {
            RawArticle articleFromDb = dbContext.RawArticles.Find(id);
            RawArticleViewModel articleToReturn = mapper.Map<RawArticleViewModel>(articleFromDb);
            return articleToReturn;
        }

        public int MakeRawArticleDraft(int articleId, string userId)
        {
            RawArticle rawArticle = dbContext.RawArticles.Find(articleId);
            Article newArticle = mapper.Map<Article>(rawArticle);
            newArticle.TempArticleId = newArticle.Id;
            newArticle.Id = 0;
            newArticle.CreationDate = DateTime.UtcNow;
            newArticle.CreatorId = userId;
            newArticle.State = Data.Enums.ArticleState.Draft;
            dbContext.Articles.Add(newArticle);
            rawArticle.IsDeleted = true;    
            dbContext.SaveChanges();

            ArticleSeoData articleToEditSeoData = new ArticleSeoData() { ArticleId = newArticle.Id };
            dbContext.ArticlesSeoData.Add(articleToEditSeoData);
            dbContext.SaveChanges();
            return newArticle.Id;
        }
    }
}
