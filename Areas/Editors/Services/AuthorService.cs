using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Areas.Editors.ViewModels.Content;
using SportBox7.Data;
using SportBox7.Data.Enums;
using SportBox7.Data.Models;
using SportBox7.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services
{
    public class AuthorService : IAuthorService
    {
      
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IMapper mapper;


        public AuthorService(ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.hostingEnvironment = hostingEnvironment;
            this.mapper = mapper;
        }

        public void AddNewDraft(AddArticleViewModel model)
        {
            string webRootPath = hostingEnvironment.WebRootPath;
            string imageUrl = @$"{webRootPath}\Images\{model?.ImageName}.jpg";
            Article article = mapper.Map<Article>(model);

            using (var fileStream = new FileStream(imageUrl, FileMode.Create))
            {
                if (model.ArticleImage != null)
                {
                    model.ArticleImage.CopyTo(fileStream);
                    byte[] myBinaryImage = File.ReadAllBytes(imageUrl);
                    var resizzedImage = SkiaSharpImageManipulationProvider.Resize(myBinaryImage, 310, 195);
                    File.WriteAllBytes(imageUrl, resizzedImage.FileContents);
                    article.ImageUrl = $@"\Images\{model.ImageName}.jpg";
                }

                article.CreationDate = DateTime.UtcNow;
                article.LastModDate = DateTime.UtcNow;
                article.State = ArticleState.Draft;
                dbContext.Articles.Add(article);
                dbContext.SaveChanges();
                
            }

            ArticleSeoData seoData = mapper.Map<ArticleSeoData>(model);
            seoData.ArticleId = dbContext.Articles.Where(x => x == article).FirstOrDefault().Id;
            dbContext.ArticlesSeoData.Add(seoData);
            dbContext.SaveChanges();
        }

        public EditArticleViewModel EditDraftGetModel(int draftId)
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

        public void EditDraft(EditArticleViewModel model)
        {
            Article draftToEdit = dbContext.Articles.Find(model.Id);
            draftToEdit.Body = model.Body;
            draftToEdit.CategoryId =model.CategoryId;
            draftToEdit.EnableComments = model.EnableComments;
            draftToEdit.H1Tag = model.H1Tag;
            draftToEdit.LastModDate = DateTime.UtcNow;
            draftToEdit.SourceName = model.SourceName;
            draftToEdit.SourceURL = model.SourceURL;
            draftToEdit.Title = model.Title;
            dbContext.SaveChanges();

            ArticleSeoData articleSeoDataToEdit = dbContext.ArticlesSeoData.Where(x => x.ArticleId == draftToEdit.Id).FirstOrDefault();
            articleSeoDataToEdit.MetaDescription = model.MetaDescription;
            articleSeoDataToEdit.MetaKeyword = model.MetaKeyword;
            articleSeoDataToEdit.MetaTitle = model.MetaTitle;
            articleSeoDataToEdit.SeoUrl = model.SeoUrl;
            dbContext.SaveChanges();
        
        }

        public void SentDraftForReview(int articleId)
        {
            Article articleForReview = dbContext.Articles.Find(articleId);
            articleForReview.State = ArticleState.ForApproval;
            dbContext.SaveChanges();
        }

        public ICollection<AllArticlesViewModel> LoadMyArticlesForReview(string userId)
        {
            ICollection<AllArticlesViewModel> articlesToReturn = new List<AllArticlesViewModel>();
            var userArticlesForReview = dbContext.Articles.Where(d => d.CreatorId == userId && d.State == ArticleState.ForApproval && d.IsDeleted == false).OrderBy(x => x.CreationDate);

            foreach (var article in userArticlesForReview)
            {
                AllArticlesViewModel tempDraftViewModel = mapper.Map<AllArticlesViewModel>(article);
                tempDraftViewModel.Category = dbContext.Categories.Where(c => c.Id == article.CategoryId).FirstOrDefault().CategoryName;
                articlesToReturn.Add(tempDraftViewModel);
            }

            return articlesToReturn;
        }
    }
}
