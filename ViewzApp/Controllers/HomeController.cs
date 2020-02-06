using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewzApp.Models;
//using ProducerAPI

namespace ViewzApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<WikiHeaderVM> list = new List<WikiHeaderVM>();
            //gathering list of info from PRODUCER API
            //getfrompublicwikis() --returns JSON

            //Parse JSON
            // - foreach(item in JSON)
            //          list.Add(new WikiHeaderVM(){name= , url = });

            //Just to test view...
            //limit = 2
            list.Add(new WikiHeaderVM() { Name = "Pokemon", URL = "https://lmgtfy.com/?q=pokemon" });
            list.Add(new WikiHeaderVM() { Name = "Zelda", URL = "https://lmgtfy.com/?q=zelda" });
            list.Add(new WikiHeaderVM() { Name = "Mario Kart", URL = "https://lmgtfy.com/?q=Mario+Kart" });
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
