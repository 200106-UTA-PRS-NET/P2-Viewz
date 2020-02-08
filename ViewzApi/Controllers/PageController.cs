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

        public PageController() 
        {
        }

        //public ContentResult Get([FromRoute] string WikiUrl, [FromRoute] string PageUrl,
        //                bool details = true, bool html = true, bool content = true)
        //{
        //    string output = "";

        //    if (Wikis.Exists(w => w.Url == WikiUrl))
        //    {
        //        var wiki = Wikis.FirstOrDefault(w => w.Url == WikiUrl);
        //        var page = new Page();

        //        //loop through all pages in the wiki
        //        foreach (var p in wiki.Page) 
        //        {
        //            //if url matches a url from the wiki, set it to page variable
        //            if (p.Url == PageUrl)
        //            {
        //                page = Pages.FirstOrDefault(p => p.Url == PageUrl);
        //            }
        //        }

        //        //if url from page variable is not null, then output page
        //        if (page.Url != null) 
        //        {
        //            output = $"<h1>Page ID: {page.PageId}</h1>";
        //            output += $"<h3>{page.PageName}</h3>";
        //            output += $"<p>{page.Content}</p>";
        //        }
        //        else
        //        {
        //            output = "<h3>page does not exist in wiki</h3>";
        //        }

        //    }
        //    else
        //    {
        //        output = "No such url exists";
        //    }


        //    return base.Content(output, "text/html");
        //}


        public Page Get([FromRoute] string WikiUrl, [FromRoute] string PageUrl,
                        bool details = true, bool html = true, bool content = true)
        {
            var page = new Page();

            if (Wikis.Exists(w => w.Url == WikiUrl))
            {
                var wiki = Wikis.FirstOrDefault(w => w.Url == WikiUrl);
                
                //loop through all pages in the wiki
                foreach (var p in wiki.Page)
                {
                    if (p.Url == PageUrl)
                    {
                        page = wiki.Page.FirstOrDefault(p => p.Url == PageUrl);
                    }
                } 
            }

            return page;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Page page)
        {
            Pages.Add(page);

            return Ok($"{page.PageId} is added");
        }
    }
}
