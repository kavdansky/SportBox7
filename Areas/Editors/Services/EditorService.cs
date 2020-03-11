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
using SkiaSharp;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading.Tasks;


namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public class EditorService : IEditorService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostingEnvironment;


        public EditorService(ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            this.dbContext = dbContext;
            this.hostingEnvironment = hostingEnvironment;
        }


        public void AddArticleForReview(AddArticleForReviewViewModel model)
        {
            

            Article article = new Article();
            string webRootPath = hostingEnvironment.WebRootPath;
            string imageUrl = @$"{webRootPath}\Images\{model.ImageName}.jpg";

            using (var fileStream = new FileStream(imageUrl, FileMode.Create))
            {
                model.ArticleImage.CopyTo(fileStream);
            }
            
            byte[] myBinaryImage = File.ReadAllBytes(imageUrl);
            var resizzedImage = SkiaSharpImageManipulationProvider.Resize(myBinaryImage, 310, 195);
            File.WriteAllBytes(imageUrl, resizzedImage.FileContents);


            article.ImageUrl = $@"\Images\{model.ImageName}.jpg";
            article.CreatorId = model.CreatorId;
            article.Body = model.Body;
            article.CreationDate = DateTime.UtcNow;
            article.EnableComments = model.EnableComments;
            article.H1Tag = model.H1Tag;
            article.CategoryId = model.CategoryId;
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

        public List<SelectListItem> GetUserCategories(IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor?.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = dbContext.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
            var userRoleId = userRole.RoleId;


            List <SelectListItem> categories = new List<SelectListItem>();



            var userPermitedCategories = this.dbContext.RolesCategories.Where(x => x.RoleId == userRoleId).ToList();


            foreach (var category in userPermitedCategories)
            {
                Category currentCategory = dbContext.Categories.Find(category.CategoryId);
                SelectListItem selListItem = new SelectListItem(currentCategory.CategoryName, currentCategory.Id + "");
                categories.Add(selListItem);
            }

            return categories;
        }

        
    }
}
