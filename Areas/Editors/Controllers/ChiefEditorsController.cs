using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Areas.Editors.ViewModels.Content;

namespace SportBox7.Areas.Editors.Controllers
{
    [Area("Editors")]
    [Authorize(Roles = "ChiefEditor")]
    public class ChiefEditorsController : Controller
    {
        

        public ChiefEditorsController()
        {
          
        }

        [Area("Editors")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}