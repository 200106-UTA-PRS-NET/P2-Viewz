using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewzApi.Models;

namespace ViewzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikiController : ControllerBase
    {
        private readonly IPageRepository _repository;

        public WikiController(IPageRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet]
        //public IEnumerable<Wiki> Get() 
        //{
        //    return Wikis;//from repository
        //}

        //GET: api/Wiki/some-url
        //[HttpGet]
        [HttpGet("{WikiURL}", Name = "GetPopularPages")]
        public IEnumerable<DataAccess.Storing.Page> Get([FromRoute]string WikiURL)
        {
             return _repository.GetPopularPages(WikiURL,5); 
        }

        /*
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


        */

        //api/wiki/url-of-wiki
        //[HttpGet("{url}", Name = "GetWiki")]
        //public Wiki Get([FromRoute]string url, [FromQuery]bool Content = true)
        //{
        //    Wiki Wiki = new Wiki();

        //    if (Wikis.Exists(w => w.Url == url))
        //    {
        //        Wiki = Wikis.FirstOrDefault(w => w.Url == url);
        //    }

        //    return Wiki;
        //}

        //public WikiHtmlDescription Get() 
        //{

        //    return _repository.WikiHtmlDescription();
        //}

        //
        //[HttpPost]
        //public IActionResult Post([FromBody]Wiki wiki)
        //{
        //    Wikis.Add(wiki);

        //    return Ok($"{ wiki.Id} is added");
        //}
    }
}
