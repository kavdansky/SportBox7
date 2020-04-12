using AutoMapper;
using SportBox7.Areas.Editors.ViewModels.Content;
using SportBox7.Data;
using SportBox7.Data.Enums;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public AdminService(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void DeleteArticle(int articleId)
        {
            Article articleToDelete = dbContext.Articles.Find(articleId);
            articleToDelete.IsDeleted = true;
            dbContext.SaveChanges();
        }

        public void EditArticle(EditArticleViewModel model)
        {
            Article articleToEdit = dbContext.Articles.Find(model.Id);
            articleToEdit.Body = model.Body;
            articleToEdit.CategoryId = model.CategoryId;
            articleToEdit.EnableComments = model.EnableComments;
            articleToEdit.H1Tag = model.H1Tag;
            articleToEdit.LastModDate = DateTime.UtcNow;
            articleToEdit.SourceName = model.SourceName;
            articleToEdit.SourceURL = model.SourceURL;
            articleToEdit.Title = model.Title;
            articleToEdit.LastModDate = DateTime.UtcNow;
            dbContext.SaveChanges();

            ArticleSeoData articleSeoDataToEdit = dbContext.ArticlesSeoData.Where(x => x.ArticleId == articleToEdit.Id).FirstOrDefault();
            articleSeoDataToEdit.MetaDescription = model.MetaDescription;
            articleSeoDataToEdit.MetaKeyword = model.MetaKeyword;
            articleSeoDataToEdit.MetaTitle = model.MetaTitle;
            articleSeoDataToEdit.SeoUrl = model.SeoUrl;
            dbContext.SaveChanges();
        }

        public EditArticleViewModel EditArticleGetModel(int draftId)
        {
            EditArticleViewModel model = new EditArticleViewModel();
            Article articleToEdit = dbContext.Articles.Find(draftId);
            ArticleSeoData articleToEditSeoData = dbContext.ArticlesSeoData.Where(a => a.ArticleId == articleToEdit.Id).FirstOrDefault();
            model = mapper.Map<EditArticleViewModel>(articleToEdit);
            model.CategoryId = articleToEdit.CategoryId;
            model.MetaDescription = articleToEditSeoData.MetaDescription;
            model.MetaKeyword = articleToEditSeoData.MetaKeyword;
            model.MetaTitle = articleToEditSeoData.MetaTitle;
            model.SeoUrl = articleToEditSeoData.SeoUrl;
            return model;
        }

        public void PublishArticle(int id)
        {
            Article articleForApprove = dbContext.Articles.Find(id);
            articleForApprove.State = ArticleState.Published;
            dbContext.SaveChanges();
        }

        public void UnPublish(int articleId)
        {
            Article articleForReturn = dbContext.Articles.Find(articleId);
            articleForReturn.State = ArticleState.ForApproval;
            dbContext.SaveChanges();
        }
    }
}
