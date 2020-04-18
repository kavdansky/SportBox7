using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Data;
using SportBox7.Data.Enums;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using SportBox7.Extensions;
using AutoMapper;
using SkiaSharp;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using SportBox7.Areas.Identity.Pages.Account;
using SportBox7.Areas.Editors.ViewModels.Content;
using System.Threading;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public class EditorService : IEditorService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IMapper mapper;


        public EditorService(ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment, IMapper mapper)
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
            
            
            if (model.ArticleImage != null)
            {
                using (var fileStream = new FileStream(imageUrl, FileMode.Create))
                {
                    model.ArticleImage.CopyTo(fileStream);
                }
                
                byte[] myBinaryImage = File.ReadAllBytes(imageUrl);
                var resizzedImage = SkiaSharpImageManipulationProvider.ResizeStaticProportions(myBinaryImage, 460);
                File.WriteAllBytes(imageUrl, resizzedImage.FileContents);
                article.ImageUrl = $@"\Images\{model.ImageName}.jpg";
            }

            article.CreationDate = DateTime.UtcNow;
            article.LastModDate = DateTime.UtcNow;
            article.State = ArticleState.Draft;
            article.CategoryId = model.CategoryId;
            dbContext.Articles.Add(article);
            dbContext.SaveChanges();

            ArticleSeoData seoData = mapper.Map<ArticleSeoData>(model);
            seoData.ArticleId = dbContext.Articles.Where(x => x == article).FirstOrDefault().Id;
            dbContext.ArticlesSeoData.Add(seoData);
            dbContext.SaveChanges();

        }

        public void DeleteDraft(int draftId)
        {
            Article draftToDelete = dbContext.Articles.Find(draftId);
            draftToDelete.IsDeleted = true;
            dbContext.SaveChanges();
        }

        public void EditDraft(EditArticleViewModel model)
        {
            Article draftToEdit = dbContext.Articles.Find(model.Id);
            draftToEdit.Body = model.Body;
            draftToEdit.CategoryId = model.CategoryId;
            draftToEdit.EnableComments = model.EnableComments;
            draftToEdit.H1Tag = model.H1Tag;
            draftToEdit.LastModDate = DateTime.UtcNow;
            draftToEdit.SourceName = model.SourceName;
            draftToEdit.SourceURL = model.SourceURL;
            draftToEdit.Title = model.Title;
            draftToEdit.LastModDate = DateTime.UtcNow;
            dbContext.SaveChanges();

            ArticleSeoData articleSeoDataToEdit = dbContext.ArticlesSeoData.Where(x => x.ArticleId == draftToEdit.Id).FirstOrDefault();
            articleSeoDataToEdit.MetaDescription = model.MetaDescription;
            articleSeoDataToEdit.MetaKeyword = model.MetaKeyword;
            articleSeoDataToEdit.MetaTitle = model.MetaTitle;
            articleSeoDataToEdit.SeoUrl = model.SeoUrl;
            dbContext.SaveChanges();
        }

        public EditArticleViewModel EditDraftGetModel(int draftId)
        {
            EditArticleViewModel model = new EditArticleViewModel();
            Article articleToEdit = dbContext.Articles.Find(draftId);
            ArticleSeoData articleToEditSeoData = dbContext.ArticlesSeoData.Where(a => a.ArticleId == articleToEdit.Id).FirstOrDefault();
            model = mapper.Map<EditArticleViewModel>(articleToEdit);
            model.CategoryId = articleToEdit.CategoryId;
            if (articleToEditSeoData != null)
            {           
                model.MetaDescription = articleToEditSeoData.MetaDescription;
                model.MetaKeyword = articleToEditSeoData.MetaKeyword;
                model.MetaTitle = articleToEditSeoData.MetaTitle;
                model.SeoUrl = articleToEditSeoData.SeoUrl;
            }
            
                
            
            
            return model;
        }



        public List<Category> GetAllCategories()
        {
            return this.dbContext.Categories.ToList();
        }

        public AllArticlesViewModel GetDeleteDraftModel(int draftId)
        {
            Article draftToDelete = dbContext.Articles.Find(draftId);
            string modelCategory = dbContext.Categories.Find(draftToDelete.CategoryId).CategoryName;
            AllArticlesViewModel model = mapper.Map<AllArticlesViewModel>(draftToDelete);
            model.Category = modelCategory;
            
            return model;

        }

        public List<SelectListItem> GetUserCategories(IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor?.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = dbContext.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
            var userRoleId = userRole.RoleId;


            List <SelectListItem> categories = new List<SelectListItem>();
            var userPermitedCategories = this.dbContext.UserCategories.Where(x => x.UserId == userId).ToList();
           
           
            foreach (var category in userPermitedCategories)
            {
                Category currentCategory = dbContext.Categories.Find(category.CategoryId);
                SelectListItem selListItem = new SelectListItem(currentCategory.CategoryName, currentCategory.Id + "");
                categories.Add(selListItem);
            }

            return categories;
        }

        public ICollection<AllArticlesViewModel> LoadAllDrafts(string userId)
        {
            ICollection<AllArticlesViewModel> draftsToReturn = new List<AllArticlesViewModel>();
            var userDrafts = dbContext.Articles.Where(d => d.CreatorId == userId && d.State == ArticleState.Draft && d.IsDeleted == false).OrderByDescending(x=> x.CreationDate);

            foreach (var draft in userDrafts)
            {
                AllArticlesViewModel tempDraftViewModel = mapper.Map<AllArticlesViewModel>(draft);
                tempDraftViewModel.Category = dbContext.Categories.Where(c => c.Id == draft.CategoryId).FirstOrDefault().CategoryName;
                draftsToReturn.Add(tempDraftViewModel);
            }

            return draftsToReturn;

        }

        public ICollection<AllArticlesViewModel> LoadAllPublishedArticles()
        {
            ICollection<AllArticlesViewModel> articlesToReturn = new List<AllArticlesViewModel>();
            var articles = dbContext.Articles.Where(d =>  d.State == ArticleState.Published && d.IsDeleted == false).OrderByDescending(x => x.CreationDate);

            foreach (var article in articles)
            {
                AllArticlesViewModel tempDraftViewModel = mapper.Map<AllArticlesViewModel>(article);
                tempDraftViewModel.Category = dbContext.Categories.Where(c => c.Id == article.CategoryId).FirstOrDefault().CategoryName;
                articlesToReturn.Add(tempDraftViewModel);
            }

            return articlesToReturn;
        }

        public ICollection<AllArticlesViewModel> LoadArticlesForReview()
        {
            ICollection<AllArticlesViewModel> articlesToReturn = new List<AllArticlesViewModel>();
            var articles = dbContext.Articles.Where(d => d.State == ArticleState.ForApproval && d.IsDeleted == false).OrderByDescending(x => x.CreationDate);

            foreach (var article in articles)
            {
                AllArticlesViewModel tempArticleToReturnViewModel = mapper.Map<AllArticlesViewModel>(article);
                tempArticleToReturnViewModel.Category = dbContext.Categories.Where(c => c.Id == article.CategoryId).FirstOrDefault().CategoryName;
                articlesToReturn.Add(tempArticleToReturnViewModel);
            }

            return articlesToReturn;
        }
    }
}
