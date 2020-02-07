using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewzApi.Models;

namespace ViewzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        static List<Page> Pages = new List<Page>()
        {
            new Page(){ Id=1, WikiId=1, Name="Page 1", Content = "Content of Wiki 1 page 1" },
            new Page(){ Id=2, WikiId=1, Name="Page 2", Content = "Content of Wiki 1 page 2" },
            new Page(){ Id=3, WikiId=2, Name="Page 1", Content = "Content of Wiki 2 page 1" }
        };

        // GET: api/Page
        [HttpGet]
        public IEnumerable<Page> Get()
        {
            return Pages;
        }

        // GET: api/Page/5
        [HttpGet("{id}", Name = "GetPage")]

        public ContentResult Get([FromHeader]int id)
        {
            string output = $"<h1>Page ID: {Pages[id].Id}</h1>";
            output += $"<h3>Wiki Id: {Pages[id].WikiId}</h3>";
            output += $"<h3>{Pages[id].Name}</h3>";
            output += $"<p>{Pages[id].Content}</p>";

            return base.Content(output, "text/html");
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]Page page)
        {
            Pages.Add(page);

            return Ok($"{page.Id} is added");
        }
    }
}
