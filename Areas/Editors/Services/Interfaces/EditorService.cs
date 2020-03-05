using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Data;
using SportBox7.Data.Enums;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public class EditorService : IEditorService
    {
        private readonly ApplicationDbContext dbContext;
        public EditorService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void AddArticleForReview(AddArticleForReviewViewModel model)
        {

          
            Article article = new Article();
            article.CreatorId = model.CreatorId;
            article.Body = model.Body;
            article.CreationDate = DateTime.UtcNow;
            article.EnableComments = model.EnableComments;
            article.H1Tag = model.H1Tag;
            article.CategoryId = int.Parse(model.Category.Value);
            article.ImageUrl = model.ImageUrl;
            article.LastModDate = DateTime.UtcNow;
            article.TempArticleId = model.TempArticleId;
            article.Title = model.Title;
            article.State = ArticleState.ForApproval;
            article.SourceName = model.SourceName;
            article.SourceURL = model.SourceURL;

            dbContext.Articles.Add(article);
            dbContext.SaveChanges();


            ArticleSeoData seoData = new ArticleSeoData();
            seoData.ArticleId = dbContext.Articles.Where(x => x == article).FirstOrDefault().Id;
            seoData.MetaDescription = model.MetaDescription;
            seoData.MetaKeyword = model.MetaKeyword;
            seoData.MetaTitle = model.MetaTitle;
            seoData.SeoUrl = model.SeoUrl;

            dbContext.ArticlesSeoData.Add(seoData);
            dbContext.SaveChanges();

        }
    }
}
