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
    [Route("api/Wiki/{WikiUrl}/{PageUrl}")]
    [ApiController]
    public class PageController : ControllerBase
    { 
        private readonly IPageRepository _repository;

        public PageController(IPageRepository repository)
        { 
            _repository = repository; 
        }

        //url from db
        //api/wiki/training-code/readme/?html=false
        public IActionResult Get([FromRoute] string WikiUrl, [FromRoute] string PageUrl,
                        bool details = true, bool html = true, bool content = true)
        {
            try
            {
                var repoPage = (html) ? _repository.GetPageWithHTML(WikiUrl,PageUrl) : _repository.GetPageWithMD(WikiUrl, PageUrl);
                 
                Page page = new Page() {
                    Content = (html) ? repoPage.HtmlContent : repoPage.MdContent,
                    Details = (details) ? repoPage.Details : null,
                    Contents = (content) ? repoPage.Contents:null,
                    WikiUrl = WikiUrl,
                    Url = PageUrl,
                    PageName = repoPage.PageName ?? PageUrl
                }; 
                 
                return Ok(page);
            }
            catch (Exception e)
            {
                base.Content($"{e.ToString()}", "text/html");
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Post([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
        {
            try
            {
                if (page.PageName != null)
                {
                    _repository.NewPage(WikiUrl, PageUrl, page.PageName, page.Content);
                }
                else
                {
                    _repository.NewPage(WikiUrl, PageUrl, page.Content);
                }
                
                return CreatedAtAction(actionName: nameof(Get), routeValues: new { WikiUrl, PageUrl }, value: null);
            }
            catch (Exception e)
            {
                base.Content($"{e.ToString()}", "text/html");
                return BadRequest();
            }
        }

        
        [HttpPatch]
        public IActionResult Patch([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
        { 
            if (page.Content == null && page.PageName == null && page.Details == null)
            {
                return BadRequest();
            }

            if (page.PageName != null)
            {
                _repository.SetName(WikiUrl, PageUrl, page.PageName);
            }

            if (page.Content != null)
            {
                _repository.SetMD(WikiUrl, PageUrl, page.Content);

            }

            if (page.Details != null) 
            {
                _repository.SetPageDetails(WikiUrl, PageUrl, page.Details);
            }

            return NoContent();
        } 
    }
}
