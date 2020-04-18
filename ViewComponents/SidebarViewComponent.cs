using Microsoft.AspNetCore.Mvc;
using SportBox7.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IArticleService articleService;

        public SidebarViewComponent(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name)

        {

        

            return View();
        }
    }
}
