using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportBox7.Areas.Editors.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}