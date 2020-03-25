using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SportBox7.Areas.Editors.Controllers
{
    [Area("Editors")]
    [Authorize(Roles = "ChiefEditor")]
    public class ChiefEditorsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}