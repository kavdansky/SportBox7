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
            }
                      
            article.CreationDate = DateTime.UtcNow;
            article.LastModDate = DateTime.UtcNow;
            article.State = ArticleState.Draft;
            dbContext.Articles.Add(article);
            dbContext.SaveChanges();

            ArticleSeoData seoData = mapper.Map<ArticleSeoData>(model);
            seoData.ArticleId = dbContext.Articles.Where(x => x == article).FirstOrDefault().Id;
            dbContext.ArticlesSeoData.Add(seoData);
            dbContext.SaveChanges();

        }



        public List<Category> GetAllCategories()
        {
            return this.dbContext.Categories.ToList();
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
            var userDrafts = dbContext.Articles.Where(d => d.CreatorId == userId && d.State == ArticleState.Draft && d.IsDeleted == false).OrderBy(x=> x.CreationDate);

            foreach (var draft in userDrafts)
            {
                AllArticlesViewModel tempDraftViewModel = mapper.Map<AllArticlesViewModel>(draft);
                tempDraftViewModel.Category = dbContext.Categories.Where(c => c.Id == draft.CategoryId).FirstOrDefault().CategoryName;
                draftsToReturn.Add(tempDraftViewModel);
            }

            return draftsToReturn;

        }

    }
}
