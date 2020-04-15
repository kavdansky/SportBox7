using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels.Content;
using SportBox7.Areas.Editors.ViewModels.Content.TheSportDbModels;

namespace SportBox7.Areas.Editors.Controllers
{
    [Authorize(Roles ="Admin, ChiefEditor, Author")]
    public class ExternalNewsSourcesController : Controller
    {
        private readonly IEditorService editorService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IExternalNewsService externalNewsService;

        public ExternalNewsSourcesController(IEditorService editorService,
            IHttpContextAccessor httpContextAccessor,
            IExternalNewsService externalNewsService)
        {
            this.editorService = editorService;
            this.httpContextAccessor = httpContextAccessor;
            this.externalNewsService = externalNewsService;
        }

        

        [Area("Editors")]
        public IActionResult Index()
        {
            return View();
        }

        [Area("Editors")]
        [HttpGet]
        public IActionResult NewsFeed()
        {
            var userPermittedCategories = editorService.GetUserCategories(httpContextAccessor).Select(c=> int.Parse(c.Value)).ToArray();
            ICollection<RawArticleViewModel> news = externalNewsService.GetExternalNews(userPermittedCategories);
            return View(news);
        }
        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> TheSportsDb(string sport)
        {
            if (sport == null)
            {
                sport = "soccer";
            }
            //var userPermittedCategories = editorService.GetUserCategories(httpContextAccessor).Select(c => int.Parse(c.Value)).ToArray();
            var result = await externalNewsService.GetAllLagues().ConfigureAwait(true);
            var allSportS = GetSports(result, sport);
            result.Leagues = result.Leagues.Where(l => l.strSport.ToLower() == sport.ToLower()).ToList();
            ViewBag.Sports = allSportS;
            return View(result);
        }

        private List<SelectListItem> GetSports(LeaguesContainer input, string sport)
        {
            List<SelectListItem> listToReturn = new List<SelectListItem>();
            HashSet<string> tempResult = new HashSet<string>();
            foreach (var sp in input.Leagues)
            {
                if (!tempResult.Contains(sp.strSport))
                {
                    tempResult.Add(sp.strSport);
                }
            }
            foreach (var item in tempResult.ToList())
            {   
                if (item == sport)
                {
                    listToReturn.Add(new SelectListItem { Text = item, Value = item, Selected = true });
                }
                else
                {
                    listToReturn.Add(new SelectListItem { Text = item, Value = item, Selected = false });
                }
            }
            return listToReturn;
        }

        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> LeagueTeams(int id)
        {
            var userPermittedCategories = editorService.GetUserCategories(httpContextAccessor).Select(c => int.Parse(c.Value)).ToArray();
            var result = await externalNewsService.GetAllLagueTeams(id).ConfigureAwait(true);
            return View(result);
        }
        [Area("Editors")]
        [HttpGet]
        public async Task<IActionResult> TeamDetails(int id)
        {
            var userPermittedCategories = editorService.GetUserCategories(httpContextAccessor).Select(c => int.Parse(c.Value)).ToArray();
            var result = await externalNewsService.GetTeamDetails(id).ConfigureAwait(true);
            return View(result);
        }

        [Area("Editors")]
        [HttpGet]
        public IActionResult RawNewsDetails(int id)
        {
            var userPermittedCategories = editorService.GetUserCategories(httpContextAccessor).Select(c => int.Parse(c.Value)).ToArray();
            var result = externalNewsService.GetRawNewsDetails(id);
            return View(result);
        }
        
        [Area("Editors")]
        public async Task<IActionResult> LoadRawArticleAsDraft(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userPermittedCategories = editorService.GetUserCategories(httpContextAccessor).Select(c => int.Parse(c.Value)).ToArray();
            var result = externalNewsService.MakeRawArticleDraft(id, userId);
            return RedirectToAction("NewsFeed");
        }


    }
}