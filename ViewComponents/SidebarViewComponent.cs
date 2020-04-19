using Microsoft.AspNetCore.Mvc;
using SportBox7.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.ViewComponents
{
    [ViewComponent(Name ="SideBar")]
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IArticleService articleService;

        public SideBarViewComponent(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await articleService.GetSiteBarViewModel().ConfigureAwait(true);         
            return View(model);
        }
    }
}
