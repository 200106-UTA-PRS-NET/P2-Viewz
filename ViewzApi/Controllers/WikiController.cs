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
        public static List<Wiki> Wikis = new List<Wiki>()
        {
            new Wiki(){ Id=1, PageName="Wiki 1",Url = "url-of-wiki-1", Description="some description 1",Page=new List<Page>()},
            new Wiki(){ Id=2, PageName="Wiki 2", Url = "url-of-wiki-2",Description="some description 2",Page=new List<Page>()},
            new Wiki(){ Id=3, PageName="Wiki 3", Url = "url-of-wiki-3",Description="some description 3",Page=new List<Page>()}
        };

        // GET: api/Wiki
        [HttpGet]
        public IEnumerable<Wiki> Get()
        {
            return Wikis;
        }

        // GET: api/Wiki/5
        [HttpGet("{url}", Name = "GetWiki")] 
        
        public ContentResult Get([FromRoute]string url, [FromQuery]bool Content=true)
        {
            string output;
            if (Wikis.Exists(w => w.Url == url)) 
            {
                var wiki = Wikis.FirstOrDefault(w => w.Url == url);

                output = $"<h1>Wiki ID: {wiki.Id}</h1>";
                output += $"<h3>{wiki.PageName}</h3>";
                output += $"<p>{wiki.Description}</p>";
            } 
            else 
            {
                output = "No url exists";
            }
            

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
