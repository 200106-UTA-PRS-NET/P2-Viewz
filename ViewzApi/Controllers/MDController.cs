using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ViewzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MDController : ControllerBase
    {
        //private readonly IPageRepository _repository;

        //public MDController(IPageRepository repository) 
        //{

        //    _repository = repository;
        //}

        ////maybe get page from page controller will be getting the html?
        //public string GetMD(string wikiURL, string pageURL) 
        //{
        //    return _repository.GetMD(wikiURL,pageURL);
        //}

        //public string GetHTML(string wikiURL, string pageURL)
        //{
        //    return _repository.GetHTML(wikiURL, pageURL);
        //}

        //public void SetMD(string wikiURL, string pageURL, string content)
        //{
        //    _repository.SetMD(wikiURL, pageURL, content);
        //}

        //public void SetHTML(string wikiURL, string pageURL, string content)
        //{
        //    _repository.SetHTML(wikiURL, pageURL, content);
        //}








        [HttpPost] 
        public ContentResult Post([FromForm]string MD)
        { 
            string output;

            if (MD.Contains("#"))
            {
                string updated = MD.Replace("#","");
                output = $"<h1>{updated}</h1>";
            }
            else
            {
                output = MD;
            }

            return base.Content(output, "text/html");
            
        }
         
    }
}