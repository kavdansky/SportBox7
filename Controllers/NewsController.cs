using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportBox7.Services.Interfaces;
using SportBox7.ViewModels.Articles;

namespace SportBox7.Controllers
{
    public class NewsController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ISocialService socialService;
        private readonly IActionContextAccessor accessor;

        public NewsController(IArticleService articleService, ISocialService socialService, IActionContextAccessor accessor)
        {
            this.articleService = articleService;
            this.socialService = socialService;
            this.accessor = accessor;
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
       
        [HttpPost]
        public string SendSocialRequest(int articleId, bool isLiked)
        {

            var ip = accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            return socialService.SetNewSocialAction(ip, isLiked, articleId);
        }
    }
}