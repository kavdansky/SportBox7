using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportBox7.Data;
using SportBox7.Data.ExternalDataModels;
using SportBox7.Services.Interfaces;
using SportBox7.ViewModels;


namespace SportBox7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly IArticleService articleServive;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IArticleService articleServive)
        {
            _logger = logger;
            this.context = context;
            this.articleServive = articleServive;    
        }

        public async Task<IActionResult> Index()
        {

            string json = "";


            using (WebClient client = new WebClient())
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home", new { area = "Editors" });
                }
            }
            ///string apiKey = "2ab3d4cc504e93fa2849ead5596dbbea36f7f4e6f0f2bc6e4899fd1dfda3b24d";
            //json = await client.DownloadStringTaskAsync($@"https://www.thesportsdb.com/api/v1/json/1/all_leagues.php").ConfigureAwait(true);
            //LeaguesContainer container = Newtonsoft.Json.JsonConvert.DeserializeObject<LeaguesContainer>(json);
            //
            //var currentLeagues = context.Leagues;


            //foreach (var league in container.Leagues)
            //{
            //    Data.Models.League newLeague = new Data.Models.League();
            //    newLeague.Id = league.idLeague;
            //    newLeague.LeagueName = league.strLeague;
            //    newLeague.SportName = league.strSport;
            //    newLeague.LeagueNameAlternate = league.strLeagueAlternate;
            //    if (!currentLeagues.Where(x=> x.Id == league.idLeague).Any())
            //    {
            //        currentLeagues.Add(newLeague);
            //    }
            //    
            //}
            //await context.SaveChangesAsync().ConfigureAwait(true);

            var model = articleServive.GetArticlesForHomePage();
            return View(model);

        }


            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
