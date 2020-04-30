using AutoMapper;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels.Content;
using SportBox7.Data;
using SportBox7.Areas.Editors.ViewModels.Content.TheSportDbModels;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<LeaguesContainer> GetAllLagues()
        {
            string json = "";
            using WebClient client = new WebClient();          
            json = await client.DownloadStringTaskAsync($@"https://www.thesportsdb.com/api/v1/json/1/all_leagues.php").ConfigureAwait(true);
            LeaguesContainer container = Newtonsoft.Json.JsonConvert.DeserializeObject<LeaguesContainer>(json);
            if (container == null)
            {
                return null;
            }
            return container;
        }

        public async Task<TeamsContainer> GetAllLagueTeams(int id)
        {
            string json = "";
            using WebClient client = new WebClient();
            json = await client.DownloadStringTaskAsync($@"https://www.thesportsdb.com/api/v1/json/1/lookup_all_teams.php?id={id}").ConfigureAwait(true);
            TeamsContainer container = Newtonsoft.Json.JsonConvert.DeserializeObject<TeamsContainer>(json);
            if (container == null)
            {
                return null;
            }
            return container;
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

            var result = rawArticlesToReturn.OrderByDescending(x => x.Date).ToList();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public RawArticleViewModel GetRawNewsDetails(int id)
        {
            RawArticle articleFromDb = dbContext.RawArticles.Find(id);
            RawArticleViewModel articleToReturn = mapper.Map<RawArticleViewModel>(articleFromDb);
            return articleToReturn;
        }

        public async Task<Team> GetTeamDetails(int id)
        {
            string json = "";
            using WebClient client = new WebClient();
            string apiKey = "2ab3d4cc504e93fa2849ead5596dbbea36f7f4e6f0f2bc6e4899fd1dfda3b24d";
            json = await client.DownloadStringTaskAsync($@"https://www.thesportsdb.com/api/v1/json/1/lookupteam.php?id={id}").ConfigureAwait(true);
            TeamsContainer container = Newtonsoft.Json.JsonConvert.DeserializeObject<TeamsContainer>(json);
            if (container.teams is null)
            {
                return null;
            }
            return container.teams[0];
            
        }

        public int? MakeRawArticleDraft(int articleId, string userId)
        {
            RawArticle rawArticle = dbContext.RawArticles.Find(articleId);
            if (rawArticle == null)
            {
                return null;
            }
            Article newArticle = mapper.Map<Article>(rawArticle);
            newArticle.TempArticleId = rawArticle.Id;
            newArticle.Id = 0;
            newArticle.CreationDate = DateTime.UtcNow;
            newArticle.CreatorId = userId;
            newArticle.State = Data.Enums.ArticleState.Draft;
            dbContext.Articles.Add(newArticle);
            rawArticle.IsDeleted = true;    
            dbContext.SaveChanges();

            ArticleSeoData articleToEditSeoData = new ArticleSeoData() { ArticleId = newArticle.Id, MetaDescription = rawArticle.Title, MetaKeyword = rawArticle.Title.Replace(" "," ,"), MetaTitle = rawArticle.Title, SeoUrl = rawArticle.Title.Replace(" ", "-") };
            dbContext.ArticlesSeoData.Add(articleToEditSeoData);
            dbContext.SaveChanges();
            return newArticle.Id;
        }


    }
}
