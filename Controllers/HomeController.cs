using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportBox7.Data;
using SportBox7.Areas.Editors.ViewModels.Content.TheSportDbModels;
using SportBox7.Services.Interfaces;
using SportBox7.ViewModels;


namespace SportBox7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly IArticleService articleService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IArticleService articleServive)
        {
            _logger = logger;
            this.context = context;
            this.articleService = articleServive;
            

        }

        public async Task<IActionResult> Index()
        {
            ViewBag.NewsWidget = articleService.GetNewsWidget();
            var model = articleService.GetArticlesForHomePage();
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

      
        public async Task<IActionResult> NotFound()
        {
            ViewBag.NewsWidget = articleService.GetNewsWidget();
            return View();
        }


    }
}
