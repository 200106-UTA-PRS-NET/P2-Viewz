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
        static List<Page> Pages = new List<Page>()
        {
            new Page(){ PageId=1, WikiId=1, PageName="Page 1", Url="url-of-page-1",Content = "Content of Wiki 1 page 1"},
            new Page(){ PageId=2, WikiId=1, PageName="Page 2", Url="url-of-page-2",Content = "Content of Wiki 1 page 2" },
            new Page(){ PageId=3, WikiId=2, PageName="Page 1", Url="url-of-page-1",Content = "Content of Wiki 2 page 1" },
            new Page(){ PageId=3, WikiId=3, PageName="Page 1", Url="url-of-page-1",Content = "Content of Wiki 3 page 1" }
        };

        static List<Wiki> Wikis = new List<Wiki>()
        {
            new Wiki(){ Id=1, PageName="Wiki 1",Url = "url-of-wiki-1", Description="some description 1",Page=new List<Page>()
            {
                Pages[0], Pages[1]
            } },
            new Wiki(){ Id=2, PageName="Wiki 2", Url = "url-of-wiki-2",Description="some description 2",Page=new List<Page>()
            {
                Pages[2]
            } },
            new Wiki(){ Id=3, PageName="Wiki 3", Url = "url-of-wiki-3",Description="some description 3",Page=new List<Page>(){
                Pages[3]
            } }
        };

        // GET: api/Wiki
        [HttpGet]
        public IEnumerable<Wiki> Get()
        {
            return Wikis;
        }

        // GET: api/Wiki/5
        //[HttpGet("{url}", Name = "GetWiki")] 

        //public ContentResult Get([FromRoute]string url, [FromQuery]bool Content=true)
        //{
        //    string output;
        //    if (Wikis.Exists(w => w.Url == url)) 
        //    {
        //        var wiki = Wikis.FirstOrDefault(w => w.Url == url);

        //        output = $"<h1>Wiki ID: {wiki.Id}</h1>";
        //        output += $"<h3>{wiki.PageName}</h3>";
        //        output += $"<p>{wiki.Description}</p>";

        //        output += "<ul>";
        //        //show pages
        //        foreach (var p in wiki.Page)
        //        {
        //            output += $"<li>{p.PageName}</li>";
        //        }

        //        output += "</ul>";
        //    } 
        //    else 
        //    {
        //        output = "No such wiki exists";
        //    }


        //    return base.Content(output, "text/html");
        //}


        [HttpGet("{url}", Name = "GetWiki")]

        public Wiki Get([FromRoute]string url, [FromQuery]bool Content = true)
        {
            Wiki Wiki = new Wiki();

            if (Wikis.Exists(w => w.Url == url))
            {
                Wiki = Wikis.FirstOrDefault(w => w.Url == url);
            }

            return Wiki;
        }




        [HttpPost]
        public IActionResult Post([FromBody]Wiki wiki)
        {
            Wikis.Add(wiki);

            return Ok($"{ wiki.Id} is added");
        }
    }
}
