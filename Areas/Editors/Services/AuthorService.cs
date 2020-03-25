using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels;
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

        public void AddArticleForReview(AddArticleForReviewViewModel model)
        {
            string webRootPath = hostingEnvironment.WebRootPath;
            string imageUrl = @$"{webRootPath}\Images\{model?.ImageName}.jpg";
            Article article = mapper.Map<Article>(model);

            using (var fileStream = new FileStream(imageUrl, FileMode.Create))
            {
                model.ArticleImage.CopyTo(fileStream);
            }

            byte[] myBinaryImage = File.ReadAllBytes(imageUrl);
            var resizzedImage = SkiaSharpImageManipulationProvider.Resize(myBinaryImage, 310, 195);
            File.WriteAllBytes(imageUrl, resizzedImage.FileContents);

            article.ImageUrl = $@"\Images\{model.ImageName}.jpg";
            article.CreationDate = DateTime.UtcNow;
            article.LastModDate = DateTime.UtcNow;
            article.State = ArticleState.ForApproval;

            dbContext.Articles.Add(article);
            dbContext.SaveChanges();


            ArticleSeoData seoData = mapper.Map<ArticleSeoData>(model);
            seoData.ArticleId = dbContext.Articles.Where(x => x == article).FirstOrDefault().Id;
            dbContext.ArticlesSeoData.Add(seoData);
            dbContext.SaveChanges();
        }
    }
}
