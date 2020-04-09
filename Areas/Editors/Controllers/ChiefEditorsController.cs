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
        
        private readonly IEditorService editorService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IValidationService validationService;
        private readonly IAdminService adminService;

        public ChiefEditorsController(IEditorService editorService,
            IHttpContextAccessor httpContextAccessor,
            IValidationService validationService,
            IAdminService adminService)
        {
            
            this.editorService = editorService;
            this.httpContextAccessor = httpContextAccessor;
            this.validationService = validationService;
            this.adminService = adminService;
        }

        [Area("Editors")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}