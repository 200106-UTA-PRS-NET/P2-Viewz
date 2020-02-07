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
    public class WikiController : ControllerBase
    {
        static List<Wiki> Wikis = new List<Wiki>()
        {
            new Wiki(){ Id=1, Name="Wiki 1", Content = "Content of Wiki 1",Pages=new List<Page>()},
            new Wiki(){ Id=2, Name="Wiki 2", Content = "Content of Wiki 2",Pages=new List<Page>()},
            new Wiki(){ Id=3, Name="Wiki 3", Content = "Content of Wiki 3",Pages=new List<Page>()}
        };

        // GET: api/Wiki
        [HttpGet]
        public IEnumerable<Wiki> Get()
        {
            return Wikis;
        }

        // GET: api/Wiki/5
        [HttpGet("{id}", Name = "GetWiki")] 

        public ContentResult Get([FromHeader]int id)
        {
            string output = $"<h1>Wiki ID: {Wikis[id].Id}</h1>";
            output += $"<h3>{Wikis[id].Name}</h3>";
            output += $"<p>{Wikis[id].Content}</p>";

            return base.Content(output, "text/html");
        }
         
        [HttpPost]
        public IActionResult Post([FromBody]Wiki wiki)
        {
            Wikis.Add(wiki);

            return Ok($"{ wiki.Id} is added");
        }
    }
}
