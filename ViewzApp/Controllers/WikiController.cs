using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ViewzApp.Controllers
{
    public class WikiController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}