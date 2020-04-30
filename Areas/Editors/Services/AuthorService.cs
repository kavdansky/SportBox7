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
        private readonly IMapper mapper;


        public AuthorService(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;           
            this.mapper = mapper;
        }

        public bool SentDraftForReview(int articleId)
        {
            Article articleForReview = dbContext.Articles.Find(articleId);
            if (articleForReview == null)
            {
                return false;
            }
            articleForReview.State = ArticleState.ForApproval;
            dbContext.SaveChanges();
            return true;
        }

        public ICollection<AllArticlesViewModel> LoadMyArticlesForReview(string userId)
        {
            ICollection<AllArticlesViewModel> articlesToReturn = new List<AllArticlesViewModel>();
            var userArticlesForReview = dbContext.Articles.Where(d => d.CreatorId == userId && d.State == ArticleState.ForApproval && d.IsDeleted == false).OrderByDescending(x => x.CreationDate);

            if (userArticlesForReview == null)
            {
                return null;
            }

            foreach (var article in userArticlesForReview)
            {
                AllArticlesViewModel tempDraftViewModel = mapper.Map<AllArticlesViewModel>(article);
                tempDraftViewModel.Category = dbContext.Categories.Where(c => c.Id == article.CategoryId).FirstOrDefault().CategoryName;
                articlesToReturn.Add(tempDraftViewModel);
            }

            return articlesToReturn;
        }

        public ICollection<AllArticlesViewModel> LoadMyPublishedArticles(string userId)
        {
            
            ICollection<AllArticlesViewModel> articlesToReturn = new List<AllArticlesViewModel>();
            var userArticlesForReview = dbContext.Articles.Where(d => d.CreatorId == userId && d.State == ArticleState.Published && d.IsDeleted == false).OrderByDescending(x => x.CreationDate);

            if (userArticlesForReview == null)
            {
                return null;
            }

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
