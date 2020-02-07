using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewzApi.Models;

namespace ViewzApi.Controllers
{
    [Route("api/Wiki/{WikiUrl}/{PageUrl}")]
    [ApiController]
    public class PageController : ControllerBase
    {
        static List<Page> Pages = new List<Page>()
        {
            new Page(){ PageId=1, WikiId=1, PageName="Page 1", Url="url-of-page-1",Content = "Content of Wiki 1 page 1", },
            new Page(){ PageId=2, WikiId=1, PageName="Page 2", Url="url-of-page-2",Content = "Content of Wiki 1 page 2" },
            new Page(){ PageId=3, WikiId=2, PageName="Page 1", Url="url-of-page-1",Content = "Content of Wiki 2 page 1" }
        };

        static List<Wiki> Wikis = new List<Wiki>()
        {
            new Wiki(){ Id=1, PageName="Wiki 1",Url = "url-of-wiki-1", Description="some description 1",Page=new List<Page>()},
            new Wiki(){ Id=2, PageName="Wiki 2", Url = "url-of-wiki-2",Description="some description 2",Page=new List<Page>()},
            new Wiki(){ Id=3, PageName="Wiki 3", Url = "url-of-wiki-3",Description="some description 3",Page=new List<Page>()}
        };


        //public string Get([FromRoute] string WikiUrl, [FromRoute] string PageUrl, 
        //                  bool details=true, bool html = true, bool content = true)
        //{

        //    return $"{WikiUrl} {PageUrl} {details} {html} {content} ";
        //}

        public ContentResult Get([FromRoute] string WikiUrl, [FromRoute] string PageUrl,
                         bool details = true, bool html = true, bool content = true)
        {
            
            string output;
            if (Pages.Exists(p => p.Url == PageUrl) && Wikis.Exists(w => w.Url == WikiUrl))
            {
                var page = Pages.FirstOrDefault(p => p.Url == PageUrl);

                output = $"<h1>Page ID: {page.PageId}</h1>";
                output += $"<h3>{page.PageName}</h3>";
                output += $"<p>{page.Content}</p>";
            }
            else
            {
                output = "No Url Exists";
            }


            return base.Content(output, "text/html");
        }
         

        [HttpPost]
        public IActionResult Post([FromBody]Page page)
        {
            Pages.Add(page);

            return Ok($"{page.PageId} is added");
        }
    }
}
