using AngleSharp;
using Microsoft.Extensions.DependencyInjection;
using SportBox7.Data;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.HostedServices
{
  

    public class BgFootballUnionService : HostedService
    {
        private const string BrowserHeader = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36"; 
        private readonly IServiceScopeFactory scopeFactory;

        public BgFootballUnionService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var news = await GetBFUThisMonthNews().ConfigureAwait(true);
                UpdateDbRawArticles(news);
                await Task.Delay(TimeSpan.FromHours(4), cancellationToken).ConfigureAwait(true);

            }
        }



        private async void UpdateDbRawArticles(ICollection<RawArticle> articles)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            

                int currentMonth = DateTime.Now.Month;
                int currentYear = DateTime.Now.Year;

                RawArticle[] currentMonthArticlesInDb = applicationDbContext.RawArticles.Where(ra => ra.Date.Year == currentYear && ra.Date.Month ==    currentMonth).ToArray();

                foreach (var article  in articles)
                {
                    bool IsArticleInDb = false;
                    for (int i = 0; i < currentMonthArticlesInDb.Length; i++)
                    {
                        if (article.SourceURL == currentMonthArticlesInDb[i].SourceURL)
                        {
                            IsArticleInDb = true;
                        }
                    }
                    if (!IsArticleInDb)
                    {
                        applicationDbContext.RawArticles.Add(article);
                    }
                }
                await applicationDbContext.SaveChangesAsync().ConfigureAwait(true);
            }
        }

        private async Task<ICollection<RawArticle>> GetBFUThisMonthNews()
        {
            List<RawArticle> latestArticles = new List<RawArticle>();
            string htmlCodeRaw = "";
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("user-agent", BrowserHeader);
                client.Headers.Add("Referer", "https://www.google.com/");
                htmlCodeRaw = client.DownloadString($"https://bfunion.bg/archive/{currentYear}/{currentMonth}/0/0");
            }

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);

            string template = @"https:\/\/bfunion.bg\/archive\/[\d]{4}\/[\d]\/[\d]{5}\/0";
            Regex reg = new Regex(template);
            HashSet<string> links = new HashSet<string>();
            MatchCollection matc = reg.Matches(htmlCodeRaw);

            foreach (Match match in matc)
            {
                if (!links.Contains(match.Value))
                {
                    links.Add(match.Value);
                }
            }

            List<string> linksList = new List<string>(links);
            List<string> result = new List<string>();

            var templ = @"https:\/\/bfunion.bg\/uploads\/[\d]{4}-[\d]{2}-[\d]{2}\/size1";
            Regex imgRegex = new Regex(templ);

            foreach (var link in linksList)
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", BrowserHeader);
                    client.Headers.Add("Referer", "https://www.google.com/");
                    string htmlRaw = client.DownloadString(link);
                    var document = await context.OpenAsync(req => req.Content(htmlRaw)).ConfigureAwait(true);
                    var articleBody = document.QuerySelector("div.tr-details");
                    var articleTitle = document.QuerySelectorAll("h2.entry-title")[1];
                    var currentDay = int.Parse(document.QuerySelector("div.date").TextContent.Split(' ')[0]);
                    var images = document.Images;
                    string imageUrl = "";
                    foreach (var image in images)
                    {
                        Match match = imgRegex.Match(image.Source);
                        if (match.Success)
                        {
                            imageUrl = image.Source;
                        }

                    }

                    latestArticles.Add(new RawArticle() {Body = articleBody.TextContent.Trim(), H1Tag = articleTitle.TextContent.Trim(),  Date = new DateTime(currentYear, currentMonth, currentDay), SourceName = "Български футболен съюз", Title = articleTitle.TextContent.Trim(), SourceURL = link, CategoryId = 1, ImageUrl = imageUrl });
                }
                await Task.Delay(TimeSpan.FromHours(4)).ConfigureAwait(true);
            }
            return latestArticles;
        }

    }
}
