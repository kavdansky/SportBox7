using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportBox7.Services.Interfaces;
using SportBox7.ViewModels.Articles;

namespace SportBox7.Controllers
{
    public class NewsController : Controller
    {
        private readonly IArticleService articleService;
      

        public NewsController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All(int id, string category, string name)
        {

            ViewBag.NewsWidget = articleService.GetNewsWidget();
            ArticleViewModel article = articleService.GetSingleArticle(id);
            return View(article);
        }
    }
}