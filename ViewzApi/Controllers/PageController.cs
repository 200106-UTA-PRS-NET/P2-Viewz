using System;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewzApi.Models;
using System.Linq;

namespace ViewzApi.Controllers
{

    public enum PageContent
    {
        Md,
        Html,
        NoContent
    }
    [Route("api/Wiki/{WikiUrl}/{PageUrl}")]
    [ApiController]
    public class PageController : ControllerBase
    { 
        private readonly IPageRepository _repository;
        private readonly ILogger _logger;
      

        public PageController(IPageRepository repository, ILogger<PageController> logger)
        { 
            _repository = repository;
            _logger = logger;
        }

        //url from db
        //api/wiki/training-code/readme/?html=false

        [HttpGet]
        public IActionResult Get([FromRoute] string WikiUrl, [FromRoute] string PageUrl,
                      PageContent html=PageContent.NoContent, bool details = true, bool content=true)
        {
            try
            { 
                var repoPage = _repository.GetPage(WikiUrl, PageUrl);
                Page page = new Page();

                if (html == PageContent.Html)
                {
                    repoPage = _repository.GetPageWithHTML(WikiUrl, PageUrl);
                    page.Content = repoPage.HtmlContent; 
                }
                else if (html==PageContent.Md)
                {
                    repoPage = _repository.GetPageWithMD(WikiUrl, PageUrl);
                    page.Content = repoPage.MdContent;
                }
                else { 
                    page.Content = null;
                }
                 
                page.Details = (details) ? repoPage.Details : null;
                page.Contents = (content) ? repoPage.Contents : null; 
                page.Url = repoPage.Url ?? PageUrl;
                page.PageName = repoPage.PageName ?? PageUrl;
            
                return Ok(page);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound("404 resource can not be found");
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

                _repository.SetPageDetails(WikiUrl, PageUrl,page.Details);

                return CreatedAtAction(actionName: nameof(Get), routeValues: new { WikiUrl, PageUrl }, value: null);
            }
            catch (Exception e)
            { 
                _logger.LogError(e.Message);
                //if request is duplicated 
                return Conflict("Post request must be unique");
            }
        }

        
        [HttpPatch]
        public IActionResult Patch([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
        {
            try
            {
                if (page.Content == null && page.PageName == null && page.Details == null)
                { 
                    return BadRequest("page values cannot all be null");
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
            }
            catch (Exception e) {
                _logger.LogError(e.Message); 
            }

            return NoContent();
        } 
    }
}
