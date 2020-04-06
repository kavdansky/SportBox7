using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SportBox7.Extensions;
using SportBox7.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBox7.Data.Models;
using SportBox7.Data;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace SportBox7.Areas.Editors.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEditorService editorService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;
        

        public HomeController(IEditorService editorService, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            this.editorService = editorService;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
                
        }

        [Authorize]
        [Area("Editors")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
    }
}